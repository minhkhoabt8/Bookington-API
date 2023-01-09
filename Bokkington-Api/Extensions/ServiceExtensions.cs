using Bookington.Core.Data;
using Bookington.Infrastructure.Mapper;
using Bookington.Infrastructure.Repositories.Implementations;
using Bookington.Infrastructure.Repositories.Interfaces;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Bookington_Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BookingtonDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("BookintonDB"),
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                );
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // For recognizing iformfiles in multiple-params controller actions
                c.SchemaGeneratorOptions.CustomTypeMappings.Add(typeof(IFormFile),
                    () => new OpenApiSchema() { Type = "file", Format = "binary" });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bookington",
                    Version = "v1.0.0",
                    Description = "Court Booking System",
                });
                c.UseInlineDefinitionsForEnums();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Add Bearer Token Here",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
            });
        }

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    RequireAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes
                            (configuration["JWT:Secret"]))
                };
            });
        }

        //Regiister Service
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
        //Register Repository
        public static void AddRepositories(this IServiceCollection services)
        {
            //AddScoped method registers the service with a scoped lifetime, the lifetime of a single request.
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
        public static void AddUOW(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        }
        public static void AddEvents(this IServiceCollection services)
        {
            //Add event
        }
        public static void ConfigureApiOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }
        public static void AddServiceFilters(this IServiceCollection services)
        {
            services.AddScoped<AutoValidateModelState>();
        }
    }
}
