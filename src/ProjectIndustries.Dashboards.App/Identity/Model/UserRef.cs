namespace ProjectIndustries.Dashboards.App.Identity.Model
{
  public class UserRef
  {
    public UserRef()
    {
    }

    public UserRef(long id)
    {
      Id = id;
    }
    
    public long Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? Picture { get; set; }
  }
}