using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.App.Services
{
  public interface IViewRenderService
  {
    Task<string> RenderAsync(string viewName, object model);
  }
}