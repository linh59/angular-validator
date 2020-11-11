using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SSC.Core;
using SSC.Core.Arguments.Providers;
using SSC.Core.Base.Bussiness.Interface;
using SSC.Core.Base.Configuration;
using SSC.Core.Base.Entity.Interfaces;
using SSC.Core.Bussiness;
using SSC.DataLayer.Base.Interfaces;
using SSC.DataLayer.Services;
using System;
using System.Text;

namespace SSC.DataLayer.DependencyInjection
{
    public static class JwtBasedAuthorizationExtensions
    {
        //public static IServiceCollection AddJwtBasedAuthorization<TUser, TRole, TUserKey, TRoleKey, TUserRole, TPermission, TPermissionKey>(this IServiceCollection services, IConfiguration configuration)
        //    where TUser : class, IUser<TUserKey>
        //    where TRole : class, IRole<TRoleKey>
        //    where TPermission : class, IPermission<TPermissionKey>
        //    where TUserRole : class, IUserRole<TUser, TRole, TUserKey, TRoleKey>
        //    where TUserKey : IComparable
        //    where TRoleKey : IComparable
        //    where TPermissionKey : IComparable
        //{
        //    if (services == null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }
        //    var rand = new Random();
        //    var key = Encoding.ASCII.GetBytes(rand.GenerateRandomString(rand.Next(5, 20)) + configuration.GetValue<string>("Security:SecretKey") + rand.GenerateRandomString(rand.Next(5, 20)));
        //    services.Configure<CoreAuthenticationOptionsConfig>(options =>
        //    {
        //        options.Audience = configuration.GetValue<string>("Security:Audience");
        //        options.ExprireInMinutes = configuration.GetValue<int>("Security:ExprireInMinutes");
        //        options.Issuer = configuration.GetValue<string>("Security:Issuer");
        //        options.SecretKey = key;
        //    });

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.SaveToken = true;
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateLifetime = true,
        //            ValidateAudience = true,
        //            ValidateIssuer = true,
        //            ValidateIssuerSigningKey = true,

        //            ClockSkew = TimeSpan.Zero,

        //            ValidAudience = configuration.GetValue<string>("Security:Audience"),
        //            ValidIssuer = configuration.GetValue<string>("Security:Issuer"),
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //        };
        //    });
        //    services.AddHttpContextAccessor();

        //    services.AddScoped<IAuthorizationHandler, RequiredPermissionHandler>();
        //    services.AddScoped<IPermissionsManager, PermissionManager<TUser, TRole, TUserKey, TRoleKey, TUserRole, TPermission, TPermissionKey>>();
        //    services.AddSingleton<ITokenManager, TokenManager>();
        //    services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        //    return services;
        //}
        //public static IServiceCollection AddJwtBasedAuthorization<TUser, TRole, TKey>(this IServiceCollection services, IConfiguration configuration)
        //    where TUser : class, IUser<TKey>
        //    where TRole : class, IRole<TKey>
        //    where TKey : IComparable
        //{
        //    return services.AddJwtBasedAuthorization<TUser, TRole, TKey, TKey, BaseUserRole<TUser, TRole, TKey, TKey>, BasePermission<int>, int>(configuration);
        //}

        //public static IServiceCollection AddJwtBasedAuthorization<TUser>(this IServiceCollection services, IConfiguration configuration)
        //    where TUser : class, IUser<int>
        //{
        //    return services.AddJwtBasedAuthorization<TUser, BaseRole<int, TUser, int>, int>(configuration);
        //}

        public static IServiceCollection AddJwtBasedAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var rand = new Random();
            //var key = Encoding.ASCII.GetBytes(rand.GenerateRandomString(rand.Next(5, 20)) + configuration.GetValue<string>("Security:SecretKey") + rand.GenerateRandomString(rand.Next(5, 20)));
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Security:SecretKey"));
            services.Configure<CoreAuthenticationOptionsConfig>(options =>
            {
                options.Audience = configuration.GetValue<string>("Security:Audience");
                options.ExprireInMinutes = configuration.GetValue<int>("Security:ExprireInMinutes");
                options.Issuer = configuration.GetValue<string>("Security:Issuer");
                options.SecretKey = key;
                options.HrAuthentication = configuration.GetValue<string>("Security:HrAuthentication");
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration.GetValue<string>("Security:Audience"),
                    ValidIssuer = configuration.GetValue<string>("Security:Issuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
            services.AddHttpContextAccessor();

            services.AddScoped<IAuthorizationHandler, AuthenticationServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            return services;

            //return services.AddJwtBasedAuthorization<BaseUser>(configuration);
        }

    }
}