using System.IO;
using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem.Image
{
  public class OptimizedImageData
    : BaseBinaryData
  {
    public OptimizedImageData(string fileName, string contentType, ImageSize size, byte[] content)
    {
      Name = fileName;
      ContentType = contentType;
      Size = size;
      Content = content;
      Length = content.Length;
    }

    public ImageSize Size { get; }
    public byte[] Content { get; }

    public override Stream OpenReadStream()
    {
      return new MemoryStream(Content);
    }
  }
}