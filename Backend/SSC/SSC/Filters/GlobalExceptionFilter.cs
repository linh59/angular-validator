using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SSC.Core.Arguments.ASPModels;
using SSC.Core.Arguments.Exceptions;
using SSC.Ultis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            ResponseModel response;
            int reponseCode = (int)HttpStatusCode.BadRequest;
            if (exception is NotFoundException notFoundException)
            {
                if (string.IsNullOrEmpty(notFoundException.Message))
                {
                    response = new ResponseModel(false, null, Constants.NotFoundResponseMessage);
                }
                else
                {
                    response = new ResponseModel(false, null, notFoundException.Message);
                }
                reponseCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is ValidationFailedException validationFailedException)
            {
                if (string.IsNullOrEmpty(validationFailedException.Message))
                {
                    response = new ResponseModel(false, null, Constants.DefaultValidationFailedMessage);
                }
                else
                {
                    response = new ResponseModel(false, null, validationFailedException.Message);
                }
                reponseCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is NotOwnException notOwnException)
            {
                if (string.IsNullOrEmpty(notOwnException.Message))
                {
                    response = new ResponseModel(false, null, Constants.NotOwnResourceDefaultErrorMessage);
                }
                else
                {
                    response = new ResponseModel(false, null, notOwnException.Message);
                }
                reponseCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response = new ResponseModel(false, null, Constants.DefaultUnhandledExceptionMessage);
                //Log.Error("Internal server error has occurred. Details: {Exception}", exception);
            }

            context.Result = new JsonResult(response);
            context.HttpContext.Response.StatusCode = reponseCode;
            context.ExceptionHandled = true;
        }
    }
}
