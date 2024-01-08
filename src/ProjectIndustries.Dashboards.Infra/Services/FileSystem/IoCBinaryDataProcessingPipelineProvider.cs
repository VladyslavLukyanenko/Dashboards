using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndustries.Dashboards.App.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem
{
  public class IoCBinaryDataProcessingPipelineProvider
    : IBinaryDataProcessingPipelineProvider
  {
    private readonly IEnumerable<IBinaryDataProcessingPipeline> _knownPipelines;

    public IoCBinaryDataProcessingPipelineProvider(IEnumerable<IBinaryDataProcessingPipeline> knownPipelines)
    {
      _knownPipelines = knownPipelines;
    }

    public IBinaryDataProcessingPipeline? GetPipeline(FileUploadsConfig cfg, IBinaryData data)
    {
      var pipelines = _knownPipelines.Where(p => p.CanProcess(cfg, data)).ToArray();
      if (pipelines.Length > 1)
      {
        throw new InvalidOperationException("Should be one processing pipeline for resource processing");
      }

      return pipelines.FirstOrDefault();
    }
  }
}