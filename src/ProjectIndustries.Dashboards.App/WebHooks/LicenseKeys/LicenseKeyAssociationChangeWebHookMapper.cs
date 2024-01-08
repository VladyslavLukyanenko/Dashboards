using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProjectIndustries.Dashboards.App.Identity.Services;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products.Events.LicenseKeys;
using ProjectIndustries.Dashboards.Core.WebHooks.Services;

namespace ProjectIndustries.Dashboards.App.WebHooks.LicenseKeys
{
  public class LicenseKeyAssociationChangeWebHookMapper : WebHookMapperBase<LicenseKeyAssociationChangeWebHookDataBase>
  {
    private readonly IUserRefProvider _userRefProvider;
    private readonly IProductRefProvider _productRefProvider;
    private readonly IPlanRefProvider _planRefProvider;

    public LicenseKeyAssociationChangeWebHookMapper(IMapper mapper, IDashboardRefProvider dashboardRefProvider,
      IWebHookPayloadFactory webHookPayloadFactory, IWebHookBindingRepository webHookBindingRepository,
      IUserRefProvider userRefProvider, IProductRefProvider productRefProvider,
      IPlanRefProvider planRefProvider)
      : base(mapper, dashboardRefProvider, webHookPayloadFactory, webHookBindingRepository)
    {
      _userRefProvider = userRefProvider;
      _productRefProvider = productRefProvider;
      _planRefProvider = planRefProvider;
    }

    public override bool CanMap(object @event) => @event is LicenseKeyAssociationChange;

    protected override async ValueTask InitializeAsync(LicenseKeyAssociationChangeWebHookDataBase data,
      CancellationToken ct)
    {
      await _userRefProvider.InitializeAsync(data.User, ct);
      await _productRefProvider.InitializeAsync(data.Product, ct);
      await _planRefProvider.InitializeAsync(data.Plan, ct);
    }
  }
}