﻿namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public abstract class SoftRemovableEntity : SoftRemovableEntity<long>
  {
    protected SoftRemovableEntity()
    {
    }

    protected SoftRemovableEntity(long id)
      : base(id)
    {
    }
  }
}