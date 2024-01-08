using AutoMapper;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Products.Services;

namespace ProjectIndustries.Dashboards.Infra.Products
{
  public class ProductsMappingProfile : Profile
  {
    public ProductsMappingProfile()
    {
      CreateMap<Dashboard, DashboardData>()
        .ReverseMap()
        .IgnoreAllPropertiesWithAnInaccessibleSetter()
        .ForMember(_ => _.LogoSrc, _ => _.Ignore())
        .ForMember(_ => _.CustomBackgroundSrc, _ => _.Ignore());

      CreateMap<Dashboard, UpdateDashboardCommand>()
        .IncludeBase<Dashboard, DashboardData>()
        .ForMember(_ => _.UploadedLogoSrc, _ => _.Ignore())
        .ForMember(_ => _.UploadedCustomBackgroundSrc, _ => _.Ignore());
      
      CreateMap<StripeIntegrationConfig, StripeIntegrationConfig>();
      CreateMap<DiscordConfig, DiscordConfig>();
      CreateMap<DiscordOAuthConfig, DiscordOAuthConfig>();
      CreateMap<HostingConfig, HostingConfig>();

      CreateMap<ProductFeature, ProductFeatureData>()
        .ForMember(_ => _.UploadedIcon, _ => _.Ignore());

      CreateMap<Product, ProductData>()
        .ForMember(_ => _.UploadedIcon, _ => _.Ignore())
        .ForMember(_ => _.UploadedLogo, _ => _.Ignore())
        .ForMember(_ => _.UploadedImage, _ => _.Ignore())
        .ForMember(_ => _.UploadedImages, _ => _.Ignore());

      // CreateMap<LicenseKey, LicenseKeyData>();
      CreateMap<DenormalizedLicenseKey, LicenseKeySnapshotData>()
        .ForMember(_ => _.Id, q => q.MapFrom(_ => _.Key.Id))
        .ForMember(_ => _.Value, q => q.MapFrom(_ => _.Key.Value))
        .ForMember(_ => _.Expiry, q => q.MapFrom(_ => _.Key.Expiry))
        // .ForMember(_ => _.ProductId, q => q.MapFrom(_ => _.Key.ProductId))
        // .ForMember(_ => _.LastAuthRequest, q => q.MapFrom(_ => _.Key.LastAuthRequest))
        // .ForMember(_ => _.SessionId, q => q.MapFrom(_ => _.Key.SessionId))
        // .ForMember(_ => _.IsTrial, q => q.MapFrom(_ => _.Key.TrialEndsAt.HasValue))
        // .ForMember(_ => _.IsSuspended, q => q.MapFrom(_ => false))
        //   // _.Key.Suspensions.Count > 0 && _.Key.Suspensions
        //     // .Any(l => !l.End.HasValue || l.End > SystemClock.Instance.GetCurrentInstant())))
        // .ForMember(_ => _.IsUnbindable,
        //   q => q.MapFrom(_ =>
        //     _.Key.UserId.HasValue && _.Key.UnbindableAfter.HasValue
        //                           && _.Key.UnbindableAfter <= SystemClock.Instance.GetCurrentInstant()))
        .ForMember(_ => _.User, q => q.MapFrom(_ => _.User == null
          ? null
          : new UserRef
          {
            Id = _.User.Id,
            Picture = _.User.Avatar,
            FullName = _.User.Name + "#" + _.User.Discriminator
          }))
        .ForMember(_ => _.PlanDesc, q => q.MapFrom(_ => _.Plan.Description))
        .ForMember(_ => _.ReleaseTitle, q => q.MapFrom(_ => _.Release == null ? null : _.Release.Title));

      CreateMap<Release, ReleaseData>()
        .ReverseMap()
        .IgnoreAllPropertiesWithAnInaccessibleSetter();

      CreateMap<Release, SaveReleaseCommand>()
        .ReverseMap()
        .IgnoreAllPropertiesWithAnInaccessibleSetter();

      CreateMap<Plan, PlanData>()
        /*.ForMember(dest => dest.IsUnbindable, opt => opt.MapFrom(src => src.IsUnbindable()))*/;
    }
  }
}