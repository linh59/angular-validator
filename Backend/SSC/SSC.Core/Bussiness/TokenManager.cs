using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSC.Core.Base.Bussiness.Interface;
using SSC.Core.Base.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SSC.Core.Bussiness
{
    public class TokenManager : ITokenManager
    {
        public const string PermissionClaimsName = "permissions";
        protected readonly IOptions<CoreAuthenticationOptionsConfig> _authOptions;
        protected readonly IDictionary<string, string> claimsCollection;
        protected IEnumerable<Claim> Claims { get => claimsCollection.Select(x => new Claim(x.Key, x.Value)); }
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IMemoryCache _cache;

        public TokenManager(IMemoryCache cache, IHttpContextAccessor httpContextAccessor, IOptions<CoreAuthenticationOptionsConfig> authOptions)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _authOptions = authOptions;
            claimsCollection = new Dictionary<string, string>();
        }


        /// <summary>
        /// Add a claims to token claims collection
        /// </summary>
        /// <param name="claims">Claim key and value</param>
        public void AddClaims(params (string key, string value)[] claims)
        {
            foreach (var (key, value) in claims)
            {
                AddOrChangeClaim(key, value);
            }
        }

        /// <summary>
        /// If the claims exists, change it with new value. Otherwise, add it to claims collections.
        /// </summary>
        /// <param name="key">Claim key</param>
        /// <param name="value">Claim value</param>
        public void AddOrChangeClaim(string key, string value)
        {
            if (!claimsCollection.ContainsKey(key))
            {
                claimsCollection.Add(key, value);
            }
            else
            {
                claimsCollection[key] = value;
            }
        }

        /// <summary>
        /// Remove a token claim from collection
        /// </summary>
        /// <param name="key"></param>
        public void RemoveClaim(string key) => claimsCollection.Remove(key);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        /// </summary>
        /// <param name="userName">Username</param>
        public void AddAuthenicaionClaim(string userName) => AddOrChangeClaim(ClaimTypes.NameIdentifier, userName);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        /// </summary>
        /// <param name="email">Email</param>
        public void AddEmailClaim(string email) => AddOrChangeClaim(ClaimTypes.Email, email);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role"
        /// </summary>
        /// <param name="roles">Role</param>
        public void AddRoleClaim(params string[] roles) => AddOrChangeClaim(ClaimTypes.Role, roles.StringJoin(","));

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role"
        /// </summary>
        /// <param name="roles">Role</param>
        public void AddRoleClaim(IEnumerable<string> roles) => AddOrChangeClaim(ClaimTypes.Role, roles.StringJoin(","));

        /// <summary>
        /// Add a claim with key <see cref="PermissionClaimsName"/>
        /// </summary>
        /// <param name="roles">Permissions</param>
        public void AddPermissionClaim(params string[] permissions) => AddOrChangeClaim(PermissionClaimsName, permissions.StringJoin(","));

        /// <summary>
        /// Add a claim with key <see cref="PermissionClaimsName"/>
        /// </summary>
        /// <param name="roles">Permissions</param>
        public void AddPermissionClaim(IEnumerable<string> permissions) => AddOrChangeClaim(PermissionClaimsName, permissions.StringJoin(","));

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public string GenerateAuthenticationToken()
        {
            AddOrChangeClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            var handler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(Claims);
            var signingKey = new SymmetricSecurityKey(_authOptions.Value.SecretKey);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _authOptions.Value.Issuer,
                Audience = _authOptions.Value.Audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512),
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(_authOptions.Value.ExprireInMinutes),
            });
            return handler.WriteToken(securityToken);
        }

        /// <summary>
        /// Make current token invalid
        /// </summary>
        public void InvalidToken()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null)
            {
                return;
            }
            var tokenId = user.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var exprire = user.FindFirstValue(JwtRegisteredClaimNames.Exp);
            if (!string.IsNullOrEmpty(tokenId) && long.TryParse(exprire, out var expTime))
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(expTime);
                _cache.Set($"Token.Blacklist.{tokenId}", exprire, date);
            }
        }

        /// <summary>
        /// Return that if current token is invalid or not
        /// </summary>
        public bool TokenIsValid
        {
            get
            {
                var tokenId = _httpContextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
                if (string.IsNullOrEmpty(tokenId) && _cache.TryGetValue($"Token.Blacklist.{tokenId}", out string _))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
