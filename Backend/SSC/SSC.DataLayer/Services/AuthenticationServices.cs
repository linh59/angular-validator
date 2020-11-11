using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSC.Core;
using SSC.Core.Arguments.Requirements;
using SSC.Core.Base.Bussiness.Interface;
using SSC.Core.Base.Configuration;
using SSC.Core.Base.Infrastructure.Interface;
using SSC.Database.Entity;
using SSC.DataLayer.Base.Interfaces;
using SSC.DataLayer.Enumerations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SSC.DataLayer.Services
{
    public class AuthenticationServices : AuthorizationHandler<PermissionRequirement>, IAuthenticationServices
    {
        private readonly ITokenManager tokenManager;
        private readonly IAsyncRepository<User> userRepository;
        private readonly IOptions<CoreAuthenticationOptionsConfig> authentiocationOptions;

        public AuthenticationServices(ITokenManager tokenManager, IAsyncRepository<User> userRepository, IOptions<CoreAuthenticationOptionsConfig> authentiocationOptions)
        {
            this.tokenManager = tokenManager;
            this.userRepository = userRepository;
            this.authentiocationOptions = authentiocationOptions;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                await Task.CompletedTask;
            }

            //if (!context.User.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti && c.Issuer == _configuration.Value.Issuer) ||
            //    !context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Issuer == _configuration.Value.Issuer))
            //{
            //    return Task.CompletedTask;
            //}

            if (!tokenManager.TokenIsValid)
            {
                context.Fail();
                await Task.CompletedTask;
            }

            var userName = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var checkResult = await HasPermission(userName, requirement.Permission);
            if (checkResult)
            {
                context.Succeed(requirement);
            }
            await Task.CompletedTask;
        }

        public async Task<bool> HasPermission(string userCode, string permission)
        {
            var user = await userRepository.GetAsync(x => x.UserCode == userCode);
            return user.Roles.Any(x => x.Role.Permissions.ToString().Contains(permission, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<bool> HasPermission(string userCode, Permissions permission)
        {
            var user = await userRepository.GetAsync(x => x.UserCode == userCode);
            return user.Roles.Any(x => x.Role.Permissions.HasFlag(permission));
        }

        public async Task<string> Login(string hrToken)
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = await httpClient.GetAsync(authentiocationOptions.Value.HrAuthentication + "/ChungThuc/" + hrToken);
            if (result.IsSuccessStatusCode)
            {
                var hrOAuthToken = await result.Content.ReadAsStringAsync();
                var authenObj = JObject.Parse(hrOAuthToken.Split(".")[1].B64Decode());
                // Don't care this task.
                // TODO: Remove this after have many user. Or change logic of this, only add user that have special permissions.
                await InsertUser(authenObj);

                tokenManager.AddAuthenicaionClaim(authenObj.Value<string>("unique_name"));
                tokenManager.AddEmailClaim(authenObj.Value<string>("email"));
                var user = await userRepository.GetAsync(x => x.UserCode == authenObj.Value<string>("unique_name"));
                if (user != null)
                {
                    tokenManager.AddRoleClaim(user.Roles.Select(x => x.Role.Permissions).Aggregate(Permissions.None, (cmb, perm) => cmb | perm).ToString());
                }

                return tokenManager.GenerateAuthenticationToken();
            }
            return string.Empty;
        }

        public async Task InsertUser(JObject authenObj)
        {
            var userCode = authenObj.Value<string>("unique_name");
            var email = authenObj.Value<string>("email");
            var user = await userRepository.GetAsync(x => x.UserCode == userCode);
            if (user == null)
            {
                await userRepository.AddAsync(new User()
                {
                    Email = email,
                    UserCode = userCode,
                });
                await userRepository.SaveAsync();
                await Task.CompletedTask;
            }
            else if (user.Email != email)
            {
                user.Email = email;
                await userRepository.SaveAsync();
            }
            await Task.CompletedTask;
        }
    }
}
