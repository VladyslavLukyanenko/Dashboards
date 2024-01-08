#!/bin/bash
cd ../src/ProjectIndustries.Dashboards.Infra && \
 dotnet ef migrations add Initial --startup-project ../ProjectIndustries.Dashboards.WebApi/