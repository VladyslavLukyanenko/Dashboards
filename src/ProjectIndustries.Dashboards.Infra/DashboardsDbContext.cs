using Microsoft.EntityFrameworkCore;

namespace ProjectIndustries.Dashboards.Infra
{
  public class DashboardsDbContext : DbContext
  {
    public DashboardsDbContext(DbContextOptions<DashboardsDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
      modelBuilder.UseSnakeCaseNamingConvention();
      base.OnModelCreating(modelBuilder);
    }
  }
}