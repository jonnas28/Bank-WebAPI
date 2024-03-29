﻿using Microsoft.AspNetCore.Identity;
using WebAPI.Context;
using WebAPI.Models;
using WebAPI.Repository.Identity;
using WebAPI.Repository;
using WebAPI.Repository.Transaction.Command;

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

            //Resolve CommandHandler
            //Wrap this code
            services.AddScoped<DepositCommandHandler>();
            services.AddScoped<WithdrawCommandHandler>();
            services.AddScoped<TransferCommandHandler>();

            // Resolve AutoMapper
            services.AddAutoMapper(typeof(Program));

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
