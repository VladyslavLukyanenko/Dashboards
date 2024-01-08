using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.WebHooks;
using ProjectIndustries.Dashboards.Core.WebHooks.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.WebHooks
{
  public class EfPublishedWebHookRepository : EfCrudRepository<PublishedWebHook>, IPublishedWebHookRepository
  {
    public EfPublishedWebHookRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }
  }
}