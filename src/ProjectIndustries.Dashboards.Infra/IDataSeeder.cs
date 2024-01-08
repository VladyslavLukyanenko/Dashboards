using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Infra
{
  public interface IDataSeeder
  {
    int Order { get; }

    Task SeedAsync();
  }
}