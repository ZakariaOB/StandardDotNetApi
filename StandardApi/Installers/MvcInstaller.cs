using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StandardApi.Authorization;
using StandardApi.Filters;
using StandardApi.Options;
using StandardApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>()); 

            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            var userSettings = new UserSettings();
            configuration.GetSection(nameof(UserSettings)).Bind(userSettings);
            services.AddSingleton(userSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("TagViewer", builder => builder.RequireClaim("tags.view", "true"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MessagePrevilege", policy =>
                {
                    policy.AddRequirements(new HaveMessagePrevilegeRequirement("message.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, HaveMessagePrevilegeHandler>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Standard API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
