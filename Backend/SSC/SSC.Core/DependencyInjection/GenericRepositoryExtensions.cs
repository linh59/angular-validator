using SSC.Core.Base.Infrastructure.Abstraction;
using SSC.Core.Base.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GenericRepositoryExtensions
    {
        public static IServiceCollection AddGenericAsyncRepository(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IAsyncRepository<,>), typeof(GenericAsyncRepository<,>), serviceLifetime));
            services.Add(new ServiceDescriptor(typeof(IAsyncRepository<>), typeof(GenericAsyncRepository<>), serviceLifetime));
            return services;
        }
        
        public static IServiceCollection AddGenericRepository(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IRepository<,>), typeof(GenericRepository<,>), serviceLifetime));
            services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(GenericRepository<>), serviceLifetime));
            return services;
        }
    }
}
