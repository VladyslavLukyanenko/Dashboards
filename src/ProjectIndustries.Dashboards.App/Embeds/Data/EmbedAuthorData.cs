using Newtonsoft.Json;

namespace ProjectIndustries.Dashboards.App.Embeds.Data
{
  public class EmbedAuthorData
  {
    [JsonProperty("name")] public string Name { get; set; } = "";

    [JsonProperty("url")] public string Url { get; set; } = "";

    [JsonProperty("icon_url")]
    public string IconUrl { get; set; } = "https://projectindustries.gg/images/logo-project-raffles.png";
  }
}