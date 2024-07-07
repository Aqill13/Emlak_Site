using EntityLayer.Entities;

namespace BusinessLayer.Abstract;

public interface IUserService
{
    Task LogInAsync(UserAdmin user);
    Task LogOutAsync(UserAdmin user);
    Task<UserAdmin> GetUserAsync();
    string GetUserName();
    Task<string> GetUserEmailAsync();
    string GetUserId();
    Task<IList<string>> GetUserRolesAsync(UserAdmin user);
    Task<IList<UserAdmin>> GetUserInRoleAsync(string role);
}
