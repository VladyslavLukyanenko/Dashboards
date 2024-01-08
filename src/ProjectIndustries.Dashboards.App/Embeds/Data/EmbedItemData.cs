#pragma warning disable 8618
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProjectIndustries.Dashboards.App.Embeds.Data
{
  public class EmbedItemData
  {
    [JsonProperty("author")] public EmbedAuthorData Author { get; set; } = new();
    [JsonProperty("color")] public long Color { get; set; }
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("url")] public string Url { get; set; } = "";
    [JsonProperty("image")] public EmbedImageData Image { get; set; } = new();
    [JsonProperty("thumbnail")] public EmbedImageData Thumbnail { get; set; } = new();
    [JsonProperty("footer")] public EmbedFooterData Footer { get; set; } = new();
    [JsonProperty("fields")] public List<EmbedFieldData> Fields { get; set; } = new();
  }
}