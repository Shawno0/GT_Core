using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT_Core.Application.Common.Models;

namespace GT_Core.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<Result<string>> CreateUserAsync(string userName, string password);

        Task<Result<string>> DeleteUserAsync(string userId);
    }
}