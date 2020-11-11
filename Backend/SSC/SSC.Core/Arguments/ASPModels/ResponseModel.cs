using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Arguments.ASPModels
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ResponseModel(bool isSuccess, T data, string message = "")
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }
    }
    public class ResponseModel : ResponseModel<object>
    {
        public ResponseModel(bool isSuccess, object data, string message = "") : base(isSuccess, data, message)
        {
        }

        public ResponseModel(ModelStateDictionary input) : base(false, null, "")
        {
            var message = new StringBuilder();
            foreach (var modelState in input.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    message.AppendLine(error.ErrorMessage);
                }
            }
            Message = message.ToString();
        }
    }
}
