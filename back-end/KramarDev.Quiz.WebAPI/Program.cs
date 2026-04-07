using KramarDev.Quiz.BLL.Services;
using KramarDev.Quiz.DAL;
using KramarDev.Quiz.DAL.Database;
using KramarDev.Quiz.DAL.Database.Tables;
using KramarDev.Quiz.DALAbstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace KramarDev.Quiz.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
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

        builder.Services.AddDbContext<QuizDbContext>(opt =>
        {
            opt.UseSqlServer(DatabaseConfig.GetConnectionString());
        });

        builder.Services.AddCors();
        builder.Services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequireUppercase = false;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireLowercase = false;
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireDigit = false;
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<QuizDbContext>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
                };
            });

        builder.Services.AddAuthorization();
        builder.Services.AddScoped<TokenService>();

        builder.Services.AddScoped<IUnitOfWork>(_ => new UnitOfWork());
        builder.Services.AddScoped<ITestService, TestService>();
        builder.Services.AddScoped<IStatisticsService, StatisticsService>();
        builder.Services.AddSingleton<IAppCacheService, AppCacheService>();
        builder.Services.AddMemoryCache();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz API V1");
                c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
            });
        }

        app.UseCors(opt =>
        {
            opt.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins([
                    "https://localhost:3000",
                    "http://localhost:3000",
                    "http://localhost:3004",
                    "http://127.0.0.1:3004",
                    "http://quiz-it.online"
                ])
                .SetPreflightMaxAge(TimeSpan.FromDays(7));
        });

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        var scope = app.Services.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        uow.UpdateDbAsync().Wait();

        app.Run();
    }
}