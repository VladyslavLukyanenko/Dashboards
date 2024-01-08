using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Identity.Services;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Controllers
{
  public class UsersController : SecuredControllerBase
  {
    private readonly IUserProvider _userProvider;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityService _identityService;

    public UsersController(IServiceProvider serviceProvider, IUserProvider userProvider, IUserRepository userRepository,
      IIdentityService identityService)
      : base(serviceProvider)
    {
      _userProvider = userProvider;
      _userRepository = userRepository;
      _identityService = identityService;
    }

    [HttpGet("{userId:long}")]
    public async ValueTask<IActionResult> GetUserById(long userId, CancellationToken ct)
    {
      var user = await _userProvider.GetUserByIdAsync(userId, ct);
      if (user == null)
      {
        return NotFound();
      }

      return Ok(user);
    }

    [HttpGet("{userId:long}/lock")]
    public async ValueTask<IActionResult> LockUserById(long userId, CancellationToken ct)
    {
      var user = await _userRepository.GetByIdAsync(userId, ct);
      if (user == null)
      {
        return NotFound();
      }

      user.ToggleLockOut(true);
      return Ok();
    }

    [HttpGet("{userId:long}/unlock")]
    public async ValueTask<IActionResult> UnlockUserById(long userId, CancellationToken ct)
    {
      var user = await _userRepository.GetByIdAsync(userId, ct);
      if (user == null)
      {
        return NotFound();
      }

      user.ToggleLockOut(false);
      return Ok();
    }

    [HttpDelete("{userId:long}")]
    public async ValueTask<IActionResult> DeleteUserById(long userId, CancellationToken ct)
    {
      var user = await _userRepository.GetByIdAsync(userId, ct);
      if (user == null)
      {
        return NotFound();
      }

      user.Remove();
      return NoContent();
    }

    [HttpPut("{userId:long}")]
    public async ValueTask<IActionResult> UpdateAsync(long userId, [FromBody] UserData data, CancellationToken ct)
    {
      var user = await _userRepository.GetByIdAsync(userId, ct);
      if (user == null)
      {
        return NotFound();
      }

      await _identityService.UpdateAsync(user, data, ct);
      return NoContent();
    }
  }
}