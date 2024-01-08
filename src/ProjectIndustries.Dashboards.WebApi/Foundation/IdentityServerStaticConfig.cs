﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using ProjectIndustries.Dashboards.WebApi.Foundation.Config;
using ProjectIndustries.Dashboards.WebApi.Services;

namespace ProjectIndustries.Dashboards.WebApi.Foundation
{
  public class IdentityServerStaticConfig
  {
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
      yield return new IdentityResources.OpenId();
      yield return new IdentityResources.Profile();
      yield return new IdentityResource("dashboards-api", "Dashboards API", new[] {JwtClaimTypes.Id});
      yield return new IdentityResource("dashboards-software", "Dashboards Software API", new[] {JwtClaimTypes.Id});
    }

    public static IEnumerable<ApiResource> GetApiResources(IdentityConfig config)
    {
      return config.Clients.Select(c => new ApiResource(c.Id, c.Name)
      {
        Enabled = true,
        UserClaims =
        {
          JwtClaimTypes.Id,
          JwtClaimTypes.Role,
          JwtClaimTypes.Email,
          JwtClaimTypes.EmailVerified,
          JwtClaimTypes.FamilyName,
          JwtClaimTypes.GivenName,
          JwtClaimTypes.BirthDate,
          JwtClaimTypes.Name,
          JwtClaimTypes.Picture,
          JwtClaimTypes.Expiration,
          JwtClaimTypes.IssuedAt,
          JwtClaimTypes.Issuer,
          JwtClaimTypes.PhoneNumber,
          JwtClaimTypes.PhoneNumberVerified,
          // JwtClaimTypes.Audience,
          ClaimTypes.Name,
          ClaimTypes.Role
        },
        Scopes =
        {
          IdentityServerConstants.StandardScopes.OfflineAccess,
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          c.Id
        }
      });
    }

    public static IEnumerable<Client> GetClients(IdentityConfig config)
    {
      var clients = config.Clients.Select(c => new Client
      {
        Enabled = true,
        ClientId = c.Id,
        ClientName = c.Name,

        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        RefreshTokenExpiration = TokenExpiration.Sliding,
        SlidingRefreshTokenLifetime = (int) c.RefreshTokenLifetime.TotalSeconds,
        AccessTokenLifetime = (int) c.AccessTokenLifetime.TotalSeconds,
        ClientSecrets = {new Secret(c.ApiSecret.Sha256())},
        RefreshTokenUsage = TokenUsage.OneTimeOnly,
        AllowedScopes =
        {
          IdentityServerConstants.StandardScopes.OfflineAccess,
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          c.Id
        },
        AlwaysSendClientClaims = true,
        AllowOfflineAccess = true,
        AlwaysIncludeUserClaimsInIdToken = true
      });

      foreach (Client client in clients)
      {
        yield return client;
      }


      yield return new Client
      {
        ClientId = "licensekey-auth.client",
        ClientSecrets = new List<Secret>
        {
          new Secret("secret".Sha256())
        },

        AccessTokenLifetime = (int) TimeSpan.FromSeconds(604800).TotalSeconds,
        AllowedGrantTypes = {LicenseKeyGrantValidator.GrantTypeName},
        AllowedScopes =
        {
          "dashboards-software"
        },
        AlwaysSendClientClaims = true,
        AlwaysIncludeUserClaimsInIdToken = true
      };

      yield return new Client
      {
        ClientId = "discord-auth.client",
        RequireClientSecret = false,

        AccessTokenLifetime = (int) TimeSpan.FromSeconds(604800).TotalSeconds,
        AllowedGrantTypes =
        {
          DiscordIdTokenGrantValidator.GrantTypeName,
          DiscordRefreshTokenGrantValidator.GrantTypeName
        },
        AllowedScopes =
        {
          "dashboards-api"
        },
        AlwaysSendClientClaims = true,
        AlwaysIncludeUserClaimsInIdToken = true
      };
    }
  }
}