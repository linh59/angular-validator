using SSC.DataLayer.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSC.DataLayer.Base.Interfaces
{
    public interface IFormServices
    {
        public Task<FormSubmitResultOutput> SubmitForm();
        public Task<FormTemplateOutput> GetFormTemplate(int id);
    }
}
