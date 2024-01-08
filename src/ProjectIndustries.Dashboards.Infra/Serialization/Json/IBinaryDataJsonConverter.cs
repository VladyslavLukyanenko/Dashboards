using System;
using Newtonsoft.Json;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;
using ProjectIndustries.Dashboards.Infra.Services.FileSystem;

namespace ProjectIndustries.Dashboards.Infra.Serialization.Json
{
  // ReSharper disable once InconsistentNaming
  public class IBinaryDataJsonConverter : JsonConverter<IBinaryData>
  {
    public override bool CanWrite => false;

    public override IBinaryData ReadJson(JsonReader reader, Type objectType, IBinaryData? existingValue,
      bool hasExistingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return null!;
      }

      if (!hasExistingValue)
      {
        existingValue = new Base64FileData();
      }

      serializer.Populate(reader, existingValue!);
      return existingValue!;
    }

    public override void WriteJson(JsonWriter writer, IBinaryData? value, JsonSerializer serializer)
    {
      throw new NotSupportedException();
    }
  }
}