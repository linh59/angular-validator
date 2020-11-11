using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SSC.Core.Arguments.ASPModels;
using SSC.Core.Arguments.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Arguments.ASPFilter
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
                    response = new ResponseModel(false, null, General.DefaultNotFoundMessage);
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
                    response = new ResponseModel(false, null, General.DefaultValidationFailedMessage);
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
                    response = new ResponseModel(false, null, General.NotOwnDocumentDefaultErrorMessage);
                }
                else
                {
                    response = new ResponseModel(false, null, notOwnException.Message);
                }
                reponseCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response = new ResponseModel(false, null, General.DefaultUnhandledExceptionMessage);
                //Log.Error("Internal server error has occurred. Details: {Exception}", exception);
            }

            context.Result = new JsonResult(response);
            context.HttpContext.Response.StatusCode = reponseCode;
            context.ExceptionHandled = true;
        }
    }

    public class GlobalAsyncExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Run(() =>
            {
                var exception = context.Exception;
                ResponseModel response;
                int reponseCode = (int)HttpStatusCode.BadRequest;
                if (exception is NotFoundException notFoundException)
                {
                    if (string.IsNullOrEmpty(notFoundException.Message))
                    {
                        response = new ResponseModel(false, null, General.DefaultNotFoundMessage);
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
                        response = new ResponseModel(false, null, General.DefaultValidationFailedMessage);
                    }
                    else
                    {
                        response = new ResponseModel(false, null, validationFailedException.Message);
                    }
                    reponseCode = (int)HttpStatusCode.BadRequest;
                }
                else if (exception is ConflictDateTimeException conflictDateTimeException)
                {
                    if (string.IsNullOrEmpty(conflictDateTimeException.Message))
                    {
                        response = new ResponseModel(false, null, "Datetime conflicted in scope, try again later.");
                    }
                    else
                    {
                        response = new ResponseModel(false, null, conflictDateTimeException.Message);
                    }
                    reponseCode = (int)HttpStatusCode.BadRequest;
                }
                else if (exception is NotOwnException notOwnException)
                {
                    if (string.IsNullOrEmpty(notOwnException.Message))
                    {
                        response = new ResponseModel(false, null, General.NotOwnDocumentDefaultErrorMessage);
                    }
                    else
                    {
                        response = new ResponseModel(false, null, notOwnException.Message);
                    }
                    reponseCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    response = new ResponseModel(false, null);
                    //Log.Error("Internal server error has occurred. Details: {Exception}", exception);
                }

                context.Result = new JsonResult(response);
                context.HttpContext.Response.StatusCode = reponseCode;
                context.ExceptionHandled = true;
            });
        }
    }
}
