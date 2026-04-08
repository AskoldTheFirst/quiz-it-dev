using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace KramarDev.Quiz.WebAPI;

public static class StartupExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Quiz API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter: Bearer {your JWT token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", document),
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddAppCors(this IServiceCollection services, string policyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(policyName, policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .SetIsOriginAllowed(_ => true);
            });
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenKey = configuration["JWTSettings:TokenKey"]
            ?? throw new InvalidOperationException("JWTSettings:TokenKey was not found.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tokenKey))
                };
            });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || env.IsStaging())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API V1");
                c.ConfigObject.AdditionalItems["persistAuthorization"] = "true";
            });
        }

        return app;
    }
}
