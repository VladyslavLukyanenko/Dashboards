namespace ProjectIndustries.Dashboards.WebApi.Foundation.Config
{
  public class DataSourceConfig
  {
    public string PostgresConnectionString { get; set; } = null!;

    public int MaxRetryCount { get; set; }
  }
}