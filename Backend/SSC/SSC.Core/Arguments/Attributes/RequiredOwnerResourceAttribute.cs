using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SSC.Core.Base.Infrastructure;
using SSC.Core.Base.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SSC.Core.Base.Entity.Interfaces;
using SSC.Core.Base.Infrastructure.Abstraction;

namespace SSC.Core.Arguments.Attributes
{
    [Obsolete("Scope đang trong quá trình phát triển", true)]
    public class RequiredOwnerResourceAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private object _entityRepositoryObject;
        private IAsyncRepository<IIdentity> _entityRepository;
        private readonly Type _TCrudRepository;

        public RequiredOwnerResourceAttribute(Type TCrudRepository)
        {
            _TCrudRepository = TCrudRepository;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            var yourDbContext = context.HttpContext.RequestServices.GetService(typeof(GenericAsyncRepository<,>));
            _entityRepositoryObject = Activator.CreateInstance(_TCrudRepository, yourDbContext);
            _entityRepository = _entityRepositoryObject as IAsyncRepository<IIdentity>;

            switch (context.HttpContext.Request.Method.ToUpper())
            {
                case "POST":
                    var body = context.HttpContext.Request.Body;
                    return Task.CompletedTask;
                case "GET":

                case "DELETE":

                default:
                    return Task.CompletedTask;
            }

        }
    }
}
