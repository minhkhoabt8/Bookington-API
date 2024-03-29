﻿using Bookington.Core.Data;
using Bookington.Infrastructure.BackgroundServices;
using Bookington.Infrastructure.Helpers;
using Bookington.Infrastructure.Hubs;
using Bookington.Infrastructure.Mapper;
using Bookington.Infrastructure.Repositories.Implementations;
using Bookington.Infrastructure.Repositories.Interfaces;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Bookington_Api.Filters;
using Bookington_Api.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Reflection;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookington_Api.Extensions
{
    ///<Summary>
    ///DB Context Extension
    ///</Summary>
    public static class ServiceExtensions
    {
        ///<Summary>
        ///Add DB Context
        ///</Summary>
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BookingtonDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("BookingtonDB"),
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                );
            });
        }
        ///<Summary>
        ///Add Swagger UI
        ///</Summary>
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
                    Version = "v1.0.1",
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

                c.UseDateOnlyTimeOnlyStringConverters();
            });
        }
        ///<Summary>
        ///Add Jwt Authentication
        ///</Summary>
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
        ///<Summary>
        /// Register Service
        ///</Summary>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ISubCourtService, SubCourtService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISmsService, SmsSpeedService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IDistrictService, DistrictServices>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IUserBalanceService, UserBalanceService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUploadFileService, FileUploadService>();
            services.AddScoped<IMomoPaymentService, MomoPaymentService>();
            services.AddScoped<IBanServices,BanService>();  
            services.AddScoped<IDashBoardService,DashBoardService>();
            services.AddScoped<IChatRoomService, ChatRoomService>();
            //Add MomoHelpers
            services.AddScoped<IMomoHelpers, MomoHelpers>();
        }

        ///<Summary>
        ///Register Repository
        ///</Summary>
        public static void AddRepositories(this IServiceCollection services)
        {
            //AddScoped method registers the service with a scoped lifetime, the lifetime of a single request.
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ISubCourtRepository, SubCourtRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<ICourtReportRepository, CourtReportRepository>();
            services.AddScoped<IUserReportRepository, UserReportRepository>();
            services.AddScoped<IUserBalanceRepository, UserBalanceRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ILoginTokenRepository, LoginTokenRepository>();
            services.AddScoped<ICourtReportResponseRepository, CourtReportResponseRepository>();
            services.AddScoped<IBanRepository, BanRepository>();
            services.AddScoped<IUserReportResponseRepository, UserReportResponseRepository>();
            services.AddScoped<ISubCourtSlotRepository, SubCourtSlotRepository>();
            services.AddScoped<ICourtImageRepository, CourtImageRepository>();
            services.AddScoped<IMomoTransactionRepository, MomoTransactionRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        }
        ///<Summary>
        ///Register Unit Of Work
        ///</Summary>
        public static void AddUOW(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
        ///<Summary>
        ///Add Auto Mapper
        ///</Summary>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        }
        ///<Summary>
        ///Add Event
        ///</Summary>
        public static void AddEvents(this IServiceCollection services)
        {
            //Add event
        }
        ///<Summary>
        ///Add Api Config
        ///</Summary>
        public static void ConfigureApiOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        ///<Summary>
        ///Add Service Filter
        ///</Summary>
        public static void AddServiceFilters(this IServiceCollection services)
        {
            services.AddScoped<AutoValidateModelState>();
        }

        ///<Summary>
        ///Add SignalR Config
        ///</Summary>
        public static void ConfigureSignalROptions(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {

                options.EnableDetailedErrors = true;
                //options.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
                //options.KeepAliveInterval = TimeSpan.FromMinutes(10);
            });
        }

        ///<Summary>
        ///Register SignalR Service
        ///</Summary>
        public static void AddSignalRService(this IServiceCollection services)
        {
            services.AddSingleton<INotificationUserHub, NotificationUserHub>();
            services.AddSingleton<IChatHub, ChatHub>();
            services.AddSingleton<Dictionary<string, string>>();
        }

        ///<Summary>
        ///Register Cron Job
        ///</Summary>
        public static void AddCronJob(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Register the NotificationCleanupJob
                var notificationJobKey = new JobKey("NotificationCleanupJob");
                q.AddJob<NotificationCleanupJob>(opts => opts.WithIdentity(notificationJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(notificationJobKey)
                    .WithIdentity("NotificationCleanupJob-trigger")
                    //.WithCronSchedule("0 0 * * 0/7 ?"));
                    //.WithCronSchedule("0 * * * * ?")); 
                    .WithCronSchedule("0 0 0 */7 * ?")); // Run every 7 days

                //Register the UnbanCourtJob
                var unbanCourtJobKey = new JobKey("UnbanCourtJob");
                q.AddJob<UnbanCourtJob>(opts => opts.WithIdentity(unbanCourtJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(unbanCourtJobKey)
                    .WithIdentity("UnbanCourtJob-trigger")
                //.WithCronSchedule("0 0 0/1 * * ?")); // run every 1 hour
                //.WithCronSchedule("0 * * * * ?"));
                    //.WithCronSchedule("0 0 * ? * *")); //Fire every hours every days
                    .WithCronSchedule("0 0 0 */7 * ?")); 
                //Register the UnbanAccountJob
                var unbanAccountJobKey = new JobKey("UnbanAccountJob");
                q.AddJob<UnbanAccountJob>(opts => opts.WithIdentity(unbanAccountJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(unbanAccountJobKey)
                    .WithIdentity("UnbanAccountJob-trigger")
                    //.WithCronSchedule("0 0 0/1 * * ?")); // run every 1 hour
                    //.WithCronSchedule("0 * * * * ?"));
                    //.WithCronSchedule("0 0 * ? * *")); //Fire every hours every days
                    .WithCronSchedule("0 0 0 */7 * ?"));
            });

           services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
