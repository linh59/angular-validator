using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Bussiness.Interface
{
    public interface ITokenManager
    {
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <returns><see cref="string"/></returns>
        string GenerateAuthenticationToken();

        /// <summary>
        /// Add a claims to token claims collection
        /// </summary>
        /// <param name="claims">Claim key and value</param>
        void AddClaims(params (string key, string value)[] claims);

        /// <summary>
        /// If the claims exists, change it with new value. Otherwise, add it to claims collections.
        /// </summary>
        /// <param name="key">Claim key</param>
        /// <param name="value">Claim value</param>
        void AddOrChangeClaim(string key, string value);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        /// </summary>
        /// <param name="userName">Username</param>
        void AddAuthenicaionClaim(string userName);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        /// </summary>
        /// <param name="email">Email</param>
        void AddEmailClaim(string mail);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role"
        /// </summary>
        /// <param name="roles">Role</param>
        void AddRoleClaim(params string[] roles);

        /// <summary>
        /// Add a claim with key "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role"
        /// </summary>
        /// <param name="roles">Role</param>
        void AddRoleClaim(IEnumerable<string> roles);

        /// <summary>
        /// Add a claim with key <see cref="PermissionClaimsName"/>
        /// </summary>
        /// <param name="roles">Permissions</param>
        void AddPermissionClaim(params string[] permissions);

        /// <summary>
        /// Add a claim with key <see cref="PermissionClaimsName"/>
        /// </summary>
        /// <param name="roles">Permissions</param>
        void AddPermissionClaim(IEnumerable<string> permissions);

        /// <summary>
        /// Remove a token claim from collection
        /// </summary>
        /// <param name="key"></param>
        void RemoveClaim(string key);

        /// <summary>
        /// Invalid current token of user
        /// </summary>
        void InvalidToken();

        /// <summary>
        /// Return that if current token is invalid or not
        /// </summary>
        bool TokenIsValid { get; }
    }
}
