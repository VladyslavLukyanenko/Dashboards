using System.IO;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Infra.Services.Cryptographic
{
  public interface ICryptographicService
  {
    Task<string> ComputeHashAsync(Stream stream);
  }
}