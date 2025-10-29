using Jay.Workflow.WebApi.Bll.Mapper;
using Jay.Workflow.WebApi.Common.Cache;
using Jay.Workflow.WebApi.Common.Consts;
using Jay.Workflow.WebApi.Common.Orms.EFCore;
using Jay.Workflow.WebApi.Common.Uow;
using Jay.Workflow.WebApi.Common.Utils;
using Jay.Workflow.WebApi.Service.Extensions;
using Jay.Workflow.WebApi.Service.Filters;
using Jay.Workflow.WebApi.Service.Middlewares;
using Jay.Workflow.WebApi.Storage.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

namespace Jay.Workflow.WebApi.Service
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            DisplayConfig();

            //HttpContext
            services.AddHttpContextAccessor();

            // DbContext
            services.AddDbContext<WorkflowDbContext>((serviceProvider, options) =>
            {
                options.UseLoggerFactory(serviceProvider)
                .UseMySql(Configuration["Db:WorkflowDb:ConnStr"], new MySqlServerVersion(new Version(8, 0, 43)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork<WorkflowDbContext>>();

            //Cache
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                string redisConnectionString = Configuration["Redis:Server"];
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });
            services.AddScoped<ICacheService>(sp =>
            {
                return new CacheService(sp.GetRequiredService<IConnectionMultiplexer>(), 0);
            });

            //Route
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Jay.Workflow.WebApi API" });
                options.SwaggerDoc("BusinessService", new OpenApiInfo { Title = "业务服务", Version = "BusinessService" });
                options.SwaggerDoc("UtilService", new OpenApiInfo { Title = "工具服务", Version = "UtilService" });

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file => options.IncludeXmlComments(file, true));

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                    Name = HttpHeaderConst.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //options.OperationFilter<AddResponseHeadersFilter>();
                //options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");

            });

            //Log
            Log.Logger = ServiceCollectionExtension.GetLogConfig("Jay.Workflow.WebApi", false);

            //Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "jay",
                    ValidateAudience = true,
                    ValidAudience = "su",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jay@19961128!Alice@19961226!Now@20251029"))
                };
            });
              

            //BusinessObject
            services.AddBussinessObjectInjection();

            //AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

            //StartUpFilter
            //services.AddTransient<IStartupFilter, MigrateStartupFilter>(); //使用EFCore Migration

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (bool.TryParse(Configuration["IsEnableSwagger"],out var isEnableSwagger) && isEnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocExpansion(DocExpansion.List);
                    c.SwaggerEndpoint("/swagger/BusinessService/swagger.json", "Jay.Workflow.WebApi Business Service");
                    c.SwaggerEndpoint("/swagger/UtilService/swagger.json", "Jay.Workflow.WebApi Util Service");
                });
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            EnumHelper.InitEnumItemDtosDic("Jay.Workflow.WebApi.Model");
            EnumHelper.InitEnumItemDtosDic("Jay.Workflow.WebApi.Common");
        }

        /// <summary>
        /// 展示环境变量
        /// </summary>
        public void DisplayConfig()
        {
            Console.WriteLine("---------开始展示环境变量");

            foreach (var c in Configuration.AsEnumerable())
            {
                Console.WriteLine("---------" + c.Key + " = " + c.Value);
            }

            Console.WriteLine("---------结束展示环境变量");
        }
    }
}
