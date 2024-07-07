using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Concrete;

public class UserManager : IUserService
{
    private readonly UserManager<UserAdmin> _userManager;
    private readonly SignInManager<UserAdmin> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }
    private ClaimsPrincipal principal => _httpContextAccessor.HttpContext?.User;

    public async Task<UserAdmin> GetUserAsync()
    {
        return await _userManager.GetUserAsync(principal);
    }

    public async Task<string> GetUserEmailAsync()
    {
        return (await GetUserAsync()).Email;
    }

    public string GetUserId()
    {
        return _userManager.GetUserId(principal);
    }

    public async Task<IList<UserAdmin>> GetUserInRoleAsync(string role)
    {
        return await _userManager.GetUsersInRoleAsync(role);
    }

    public string GetUserName()
    {
        return _userManager.GetUserName(principal);
    }

    public async Task<IList<string>> GetUserRolesAsync(UserAdmin user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public Task LogInAsync(UserAdmin user)
    {
        return _signInManager.SignInAsync(user, false);
    }

    public Task LogOutAsync(UserAdmin user)
    {
        return _signInManager.SignOutAsync();
    }
}
