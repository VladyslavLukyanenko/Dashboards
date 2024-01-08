using Newtonsoft.Json;

namespace ProjectIndustries.Dashboards.App.Embeds.Data
{
  public class EmbedImageData
  {
    [JsonProperty("url")] public string Url { get; set; } = "";
  }
}