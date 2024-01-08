using System;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Analytics;
using ProjectIndustries.Dashboards.Core.Analytics.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Analytics.Services
{
  public class EfUserSessionRepository : EfCrudRepository<UserSession, Guid>, IUserSessionRepository
  {
    public EfUserSessionRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }
  }
}