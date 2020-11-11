using SSC.DataLayer.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSC.DataLayer.Base.Interfaces
{
    public interface IAuthenticationServices
    {
        /// <summary>
        /// Check if user has a specific permission
        /// </summary>
        /// <param name="userCode">User code</param>
        /// <param name="permission">Permission to check</param>
        /// <returns></returns>
        Task<bool> HasPermission(string userCode, string permission);

        /// <summary>
        /// Check if user has a specific permission
        /// </summary>
        /// <param name="userCode">User code</param>
        /// <param name="permission">Permission to check</param>
        /// <returns></returns>
        Task<bool> HasPermission(string userCode, Permissions permission);

        /// <summary>
        /// Login with HR Token
        /// </summary>
        /// <param name="hrToken">Provide HR Token</param>
        /// <returns>SSC Token</returns>
        Task<string> Login(string hrToken);
    }
}
