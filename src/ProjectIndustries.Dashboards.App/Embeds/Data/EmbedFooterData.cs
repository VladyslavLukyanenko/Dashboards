#pragma warning disable 8618
using Newtonsoft.Json;

namespace ProjectIndustries.Dashboards.App.Embeds.Data
{
  public class EmbedFooterData
  {
    [JsonProperty("text")] public string Text { get; set; }
    [JsonProperty("icon_url")] public string IconUrl { get; set; }
  }
}