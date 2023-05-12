using System.IdentityModel.Tokens.Jwt;
using GT_Core.Application.Common.Models;

namespace GT_Core.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<Result<JwtSecurityToken>> LoginAsync(string _username, string _password);
        Task<Result<bool>> AddToRoleAsync(string _role, string _username);
        Task<Result<string>> GetUserNameAsync(string _userId);

        Task<Result<bool>> IsInRoleAsync(string _userId, string _role);

        Task<Result<bool>> AuthorizeAsync(string _userId, string _policyName);

        Task<Result<string>> CreateUserAsync(string _userName, string _password);

        Task<Result<string>> DeleteUserAsync(string _userId);
    }
}