using GT_Core.Application.Common.Interfaces;
using GT_Core.Application.Common.Models;
using GT_Core.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GT_Core.Presentation.Services
{
    public class UserServiceClient : EntityServiceClient<string, ApplicationUser>, IUserService
    {
        public UserServiceClient(
            IConfiguration _config, 
            IHttpClientFactory _clientFactory,
            ITokenHandler _tokenService)
            : base(_config, _tokenService, _clientFactory, _endpoint: "account")
        {

        }

        public async Task<Result<JwtSecurityToken>> LoginAsync(string _username, string _passwordHash)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ServiceUri}/login/{_username}/{_passwordHash}");

            var response = await Client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(result);

                return new Result<JwtSecurityToken>(true, token);
            }

            return new Result<JwtSecurityToken>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public async Task<Result<bool>> AddToRoleAsync(string _role, string _username)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ServiceUri}/addtorole/{_username}/{_role}");

            var response = await Client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                bool? result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

                return new Result<bool>(result ?? false);
            }

            return new Result<bool>(false, new List<string>() { response.StatusCode.ToString() });
        }

        public Task<Result<string>> GetUserNameAsync(string _userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> IsInRoleAsync(string _userId, string _role)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> AuthorizeAsync(string _userId, string _policyName)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> CreateUserAsync(string _userName, string _password)
        {
            throw new NotImplementedException();
        }

        public Task<Result<string>> DeleteUserAsync(string _userId)
        {
            throw new NotImplementedException();
        }
    }
}
