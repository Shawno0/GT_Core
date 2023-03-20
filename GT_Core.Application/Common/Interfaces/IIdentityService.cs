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
        Task<JWToken> AuthenticateAsync(string _userName, string _password, bool _persistent);
        Task<string> GetUserNameAsync(string _userId);

        Task<bool> IsInRoleAsync(string _userId, string _role);

        Task<bool> AuthorizeAsync(string _userId, string _policyName);

        Task<Result<string>> CreateUserAsync(string _userName, string _password);

        Task<Result<string>> DeleteUserAsync(string _userId);
    }
}