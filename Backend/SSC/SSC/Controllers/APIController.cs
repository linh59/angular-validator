using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSC.Core.Arguments.ASPModels;

namespace SSC.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        protected JsonResult Json<T>(ResponseModel<T> responseModel)
        {
            return new JsonResult(responseModel);
        }

        protected JsonResult Json<T>(bool isSuccess, T data, string message = "")
        {
            return Json(new ResponseModel<T>(isSuccess, data, message));
        }

        protected JsonResult FailedJson(string message = "")
        {
            return Json<object>(false, null, message);
        }
    }
}
