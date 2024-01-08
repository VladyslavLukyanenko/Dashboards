﻿namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public abstract class AuditableEntity
    : AuditableEntity<long>
  {
    protected AuditableEntity()
    {
    }

    protected AuditableEntity(long id)
      : base(id)
    {
    }
  }
}