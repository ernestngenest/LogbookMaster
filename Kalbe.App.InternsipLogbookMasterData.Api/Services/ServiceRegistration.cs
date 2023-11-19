using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Models.Commons;
using Kalbe.App.InternsipLogbookMasterData.Api.Services.ClientService;
using Kalbe.App.InternsipLogbookMasterData.Api.Utilities;
using Kalbe.Library.Common.Logs;
using Kalbe.Library.Data.EntityFrameworkCore.Data;
using Kalbe.Library.Message.Bus;
using Kalbe.Library.Message.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Reflection;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRequiredServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<InternsipLogbookMasterDataDataContext>(name: "Database")
                .AddRedis(configuration.GetConnectionString("RedisServer"), name: "Redis Cache");

            services.AddControllers().AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                opt =>
                {
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                    // TODO: Change this swagger document definition
                    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Logbook Master Data",
                        Description = "api for services in master data",
                        Version = "v1",
                    });
                });

            services.AddStackExchangeRedisCache(
                opt =>
                {
                    opt.Configuration = configuration.GetConnectionString("RedisServer");
                });

            services.AddHttpContextAccessor();
            services.RegisterLogger(configuration, environment);
            services.RegisterDatabase<InternsipLogbookMasterDataDataContext>(configuration, environment, DatabaseProvider.PostgreSql, "PostgreSqlConnectionString");

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var config = configuration.GetSection("RabbitMQ").Get<RabbitMqConfiguration>();
                    cfg.Host(config.HostName, config.VirtualHost,
                        configurator =>
                        {
                            configurator.Username(config.UserName);
                            configurator.Password(config.Password);
                        });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(configuration, scopeFactory);
            });

            services.Configure<AppSettingModel>(opt => configuration.GetSection("ApiUrl").Bind(opt));
            services.Configure<JwtConfiguration>(opt => configuration.GetSection("JwtConfiguration").Bind(opt));

            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IUserExternalService, UserExternalService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILoggerHelper, LoggerHelper>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IAllowanceService, AllowanceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IApprovalService, ApprovalService>();
            services.AddScoped<IApprovalDetaiilService, ApprovalDetailService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUserInternalService, UserInternalService>();
            services.AddScoped<IUserRoleService, UserRoleService>();


            services.AddHttpClient<IAuthClientService, AuthClientService>();
            services.AddHttpClient<IUserProfileClientService, UserProfileClientService>();

            return services;
        }

        public static void MigrateDbContext<T>(this IApplicationBuilder applicationBuilder) where T : DbContext
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
        }
    }
}
