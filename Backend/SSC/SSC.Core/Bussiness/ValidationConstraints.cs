using SSC.Core.Base.Bussiness.Interface;
using SSC.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Core.Bussiness
{
    public class ValidationConstraints
    {
        public ValidationType ValidationType { get; set; }
        public string RootValue { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationConstraints() { }
        public ValidationConstraints(IValidation validation)
        {
            ValidationType = validation.GetValidationType();
            var hasRootVaue = validation.GetType().GetInterfaces()
                .Any(x => x.IsGenericType &&
                            x.GetGenericTypeDefinition() == typeof(IValidation<>));
            RootValue = hasRootVaue ?
                validation.GetType().GetProperty("RootValue").GetValue(validation)?.ToString() : "";
            ErrorMessage = validation.ErrorMessage;
        }
    }
}
