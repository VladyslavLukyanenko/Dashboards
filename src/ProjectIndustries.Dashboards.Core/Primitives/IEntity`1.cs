namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public interface IEntity<out TKey>
  {
    TKey Id { get; }
  }
}