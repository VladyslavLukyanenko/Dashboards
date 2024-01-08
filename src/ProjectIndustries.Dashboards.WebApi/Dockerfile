FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /profiles
COPY ./src ./
WORKDIR ProjectIndustries.Dashboards.WebApi
RUN dotnet restore && dotnet publish -c Release -o ../dist

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/dist .
ENTRYPOINT ["dotnet", "ProjectIndustries.Dashboards.WebApi.dll"]
EXPOSE 443
EXPOSE 80
