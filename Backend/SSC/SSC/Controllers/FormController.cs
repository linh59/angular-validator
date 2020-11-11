using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSC.Core.Arguments.ASPModels;
using SSC.Database.Entity;
using SSC.DataLayer.Base.Interfaces;
using SSC.DataLayer.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.Controllers
{
    public class FormController : APIController
    {
        private readonly IFormServices formServices;

        public FormController(IFormServices formServices)
        {
            this.formServices = formServices;
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel<FormTemplateOutput>> GetFormTemplate(int id)
        {
            return new ResponseModel<FormTemplateOutput>(true, await formServices.GetFormTemplate(id), "Success");
        } 

        
    }
}
