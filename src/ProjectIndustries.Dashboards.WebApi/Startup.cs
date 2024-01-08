using System;
using AspNetCoreRateLimit;
using Autofac;
using EasyCaching.InMemory;
using EFCoreSecondLevelCacheInterceptor;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectIndustries.Dashboards.App;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Infra.Products.Consumers;
using ProjectIndustries.Dashboards.WebApi.Foundation;
using ProjectIndustries.Dashboards.WebApi.Foundation.Config;
using ProjectIndustries.Dashboards.WebApi.Foundation.SwaggerSupport.Swashbuckle;
using ProjectIndustries.Dashboards.WebApi.Services;
using Quartz;

namespace ProjectIndustries.Dashboards.WebApi
{
  public class Startup
  {
    private const string ApiVersion = "v1";
    private const string ApiTitle = "Dashboards API";
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
      _configuration = configuration;
      _environment = environment;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterAssemblyModules(GetType().Assembly);
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      const string providerName = "GlobalInMemoryEfCoreCache";
      services.AddEFSecondLevelCache(o => o.UseEasyCachingCoreProvider(providerName)
        .DisableLogging(!_environment.IsDevelopment())
        /*.CacheAllQueries(CacheExpirationMode.Sliding, TimeSpan.FromSeconds(15))*/);
      
      services.AddEasyCaching(o =>
      {
        o.UseInMemory(c =>
        {
          c.DBConfig = new InMemoryCachingOptions();
        }, providerName);
      });
      services
        .InitializeConfiguration(_configuration)
        .AddApplicationDbContext(_configuration)
        .AddConfiguredRateLimiter(_configuration)
        .AddConfiguredCors(_configuration)
        .AddConfiguredMvc()
        .AddConfiguredSignalR()
        .AddConfiguredAuthentication(_configuration)
        .AddConfiguredSwagger(ApiVersion, ApiTitle)
        .AddHttpClient(NamedHttpClients.DiscordClient);


      var s = _configuration.GetSection("IdentityServer");
      services.Configure<IdentityConfig>(s);
      var cfg = new IdentityConfig();
      s.Bind(cfg);

      services.AddConfiguredAspNetIdentity(_configuration);

      services.AddIdentityServer(options =>
        {
          var ssoCfg = _configuration.GetSection("Sso").Get<SsoConfig>();
          options.EmitStaticAudienceClaim = true;
          options.IssuerUri = ssoCfg.ValidIssuer;
        })
        .AddExtensionGrantValidator<DiscordIdTokenGrantValidator>()
        .AddExtensionGrantValidator<DiscordRefreshTokenGrantValidator>()
        .AddExtensionGrantValidator<LicenseKeyGrantValidator>()
        .AddAspNetIdentity<User>()
        .AddDeveloperSigningCredential()
        .AddInMemoryClients(IdentityServerStaticConfig.GetClients(cfg))
        .AddInMemoryApiResources(IdentityServerStaticConfig.GetApiResources(cfg))
        .AddInMemoryIdentityResources(IdentityServerStaticConfig.GetIdentityResources())
        .AddProfileService<ProfileService>();

      services.AddAuthorization(configure =>
      {
        configure.AddPolicy("SoftwareClientsOnly", b =>
          b.RequireScope("dashboards-software")
            .AddAuthenticationSchemes("LicenseKey")
            .RequireAuthenticatedUser());
      });

      services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
        .AddScoped<IProfileService, ProfileService>();
      services.AddIdentityServerConfiguredCors(_configuration);

      // RateLimiting
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

      services.AddMassTransit(_ =>
        {
          _.AddConsumer<LicenseKeyAssociationChangeConsumer>();
          _.SetKebabCaseEndpointNameFormatter();
          _.UsingRabbitMq((ctx, c) =>
          {
            // var schedulerConfig = ctx.GetService<SchedulerConfig>();
            c.ConfigureEndpoints(ctx);
            // c.UseInMemoryScheduler(); //schedulerConfig.QueueName);
          });
        })
        .AddMassTransitHostedService();

      services.Configure<QuartzOptions>(_configuration.GetSection("Quartz"));
      services.AddQuartz(_ =>
      {
        _.UseMicrosoftDependencyInjectionScopedJobFactory(c =>
        {
          c.CreateScope = true;
          c.AllowDefaultConstructor = true;
        });
      });

      services.AddQuartzServer(_ =>
      {
        _.WaitForJobsToComplete = true;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // RateLimiting
      app.UseClientRateLimiting();
      app.UseResponseCompression();

      app.UseConfiguredApm(_configuration);
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      var corsConfig = app.UseCommonHttpBehavior(env);
      app.UseStaticFiles();
      app.UseIdentityServer();
      app.UseRouting();
      app.UseConfiguredCors(corsConfig);

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseAudit();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapDefaultControllerRoute();
      });

      app.UseConfiguredSwagger(ApiVersion, ApiTitle);
    }
  }
}