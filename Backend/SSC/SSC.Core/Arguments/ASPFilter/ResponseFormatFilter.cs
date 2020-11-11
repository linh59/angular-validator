using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Arguments.ASPFilter
{
    [Obsolete("Scope đang trong quá trình phát triển", true)]
    public class ResponseFormatFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(
                headerName, new string[] { "ResultExecutingSuccessfully" });
            //_logger.LogInformation("Header added: {HeaderName}", headerName);
        }
    }
}
