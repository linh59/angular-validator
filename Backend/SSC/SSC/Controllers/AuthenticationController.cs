using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSC.Core.Arguments.ASPModels;
using SSC.DataLayer.Base.Interfaces;
using SSC.DataLayer.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SSC.Controllers
{
    public class AuthenticationController : APIController
    {
        private readonly IAuthenticationServices authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            this.authenticationServices = authenticationServices;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseModel<string>> Login([FromBody] LoginInput loginModel)
        {
            var token = await authenticationServices.Login(loginModel.HrToken);
            if (string.IsNullOrEmpty(token))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return new ResponseModel<string>(false, token, "Login Failed");
            }
            return new ResponseModel<string>(true, token, "Login Successfully");
        } 
    }
}
