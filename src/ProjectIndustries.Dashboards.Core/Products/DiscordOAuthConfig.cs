namespace ProjectIndustries.Dashboards.Core.Products
{
  public class DiscordOAuthConfig
  {
    public string AuthorizeUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string RedirectUrl { get; set; } = null!;
    public string Scope { get; set; } = null!;
  }
}