using Microsoft.AspNetCore.Identity;
using WebAPI.Context;
using WebAPI.Models;
using WebAPI.Repository.Identity;
using WebAPI.Repository;

namespace WebAPI.Utils
{
    public static class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            // For Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            //CONFIGURE CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
