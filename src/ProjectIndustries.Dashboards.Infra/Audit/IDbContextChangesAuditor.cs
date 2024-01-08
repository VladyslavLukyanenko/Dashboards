using Microsoft.EntityFrameworkCore;

namespace ProjectIndustries.Dashboards.Infra.Audit
{
  public interface IDbContextChangesAuditor
  {
    void AuditChanges(DbContext ctx);
  }
}