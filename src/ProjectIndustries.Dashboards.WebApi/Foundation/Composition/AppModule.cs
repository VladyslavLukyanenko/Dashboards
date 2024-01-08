using Autofac;
using ProjectIndustries.Dashboards.App.Identity.Services;
using ProjectIndustries.Dashboards.App.Services;
using ProjectIndustries.Dashboards.Core;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Services;
using ProjectIndustries.Dashboards.WebApi.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Composition
{
  public class AppModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(User).Assembly)
        .Where(_ => _.Namespace?.Contains(".Services") ?? false)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(typeof(AspNetIdentityUserManager).Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .Except<MvcPermissionsRegistry>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterType<MvcPermissionsRegistry>()
        .AsImplementedInterfaces()
        .SingleInstance();

      //
      // builder.RegisterAssemblyTypes(typeof(UserData).Assembly)
      //   .InNamespaceOf<AspNetIdentityUserManager>()
      //   .AsImplementedInterfaces()
      //   .InstancePerLifetimeScope();
      //
      // builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
      //   .InNamespaceOf<UserService>()
      //   .AsImplementedInterfaces()
      //   .InstancePerLifetimeScope();
    }
  }
}