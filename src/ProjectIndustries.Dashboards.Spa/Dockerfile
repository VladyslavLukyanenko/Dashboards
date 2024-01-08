# build client app
FROM node:12.18.3 AS profiles-angular-build-env
     
ENV NPM_CONFIG_LOGLEVEL warn
ARG app_env

WORKDIR /angular-tmp
COPY ./src/Checkout.Movie.Profiles.Spa/ClientApp .
RUN npm i && npm run build:prod

# build backend
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS profiles-dotnet-build-env
WORKDIR /profiles
COPY ./src ./

WORKDIR Checkout.Movie.Profiles.Spa
RUN dotnet restore && dotnet publish -c Release -o ../dist

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

WORKDIR /app
COPY --from=profiles-dotnet-build-env /profiles/dist .
COPY --from=profiles-angular-build-env /angular-tmp/dist ./ClientApp/dist

ENTRYPOINT ["dotnet", "Checkout.Movie.Profiles.Spa.dll"]
EXPOSE 443
EXPOSE 80
