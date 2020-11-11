using SSC.Core.Base.Infrastructure.Interface;
using SSC.Database.Entity;
using SSC.DataLayer.Base.Interfaces;
using SSC.DataLayer.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSC.DataLayer.Services
{
    public class FormServices : IFormServices
    {
        public delegate void OnFormSubmittedDelegate(User sender, EventArgs args);
        public static event OnFormSubmittedDelegate OnFormSubmitted; 
        public delegate void OnFormSubmittingDelegate(User sender, EventArgs args);
        public static event OnFormSubmittingDelegate OnFormSubmitting;

        private readonly IAsyncRepository<FormTemplate> formTemplateRepo;

        public FormServices(IAsyncRepository<FormTemplate> formTemplateRepo)
        {
            this.formTemplateRepo = formTemplateRepo;
        }

        public Task<FormSubmitResultOutput> SubmitForm()
        {
            throw new NotImplementedException();
        }

        public async Task<FormTemplateOutput> GetFormTemplate(int id)
        {
            var template = await formTemplateRepo.GetByIdAsync(id);
            return new FormTemplateOutput()
            {
                Id = template.Id,
                Name = template.Name,
                Controls = template.Controls.Select(x => new ControlOutput(x)),
                TableControls = template.TableControls.Select(x => new ControlOutput(x))
            };
        }

        //public async Task<FormTemplate> SubmitAForm()
        //{

        //}
    }
}
