using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Arguments.Attributes
{
    /// <summary>
    /// <inheritdoc/>
    /// Implemented from <see cref="AuthorizeAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiredPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredPermissionAttribute"/>
        /// </summary>
        /// <param name="prefix">Group prefix</param>
        /// <param name="permissions">Permission of policy</param>
        //public RequiredPermissionAttribute(string prefix, Permissions permissions)
        //{
        //    Policy = $"{General.PermissionsPrefix}.{prefix}.{(byte)permissions}";
        //}
    }
}
