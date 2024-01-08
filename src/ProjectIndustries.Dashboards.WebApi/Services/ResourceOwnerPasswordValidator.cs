﻿using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;

namespace ProjectIndustries.Dashboards.WebApi.Services
{
  public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
  {
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public ResourceOwnerPasswordValidator(UserManager<User> userManager, IUserRepository userRepository)
    {
      _userManager = userManager;
      _userRepository = userRepository;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
      var user = await _userRepository.GetByEmailAsync(context.UserName);
      if (user == null)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidTarget);
        return;
      }

      if (!await _userManager.CheckPasswordAsync(user, context.Password))
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        return;
      }

      context.Result = new GrantValidationResult(user.Id.ToString(), "custom", DateTime.UtcNow);
    }
  }
}