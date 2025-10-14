using Jay.NC.Workflow.WebApi.Common.Consts;
using Jay.NC.Workflow.WebApi.Common.Orms.EFCore;
using Jay.NC.Workflow.WebApi.Common.Uow;
using Jay.NC.Workflow.WebApi.Common.Utils;
using Jay.NC.Workflow.WebApi.Service.Extensions;
using Jay.NC.Workflow.WebApi.Service.Filters;
using Jay.NC.Workflow.WebApi.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Jay.NC.Workflow.WebApi.Service
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            DisplayConfig();

            // DbContext
            services.AddDbContext<WorkflowDbContext>((serviceProvider, options) =>
            {
                options.UseLoggerFactory(serviceProvider)
                .UseMySql(Configuration["Db:WorkflowDb:ConnStr"], new MySqlServerVersion(new Version(8, 0, 43)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork<WorkflowDbContext>>();

            //Route
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Jay.NC.Workflow.WebApi API" });
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

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");

            });

            //BusinessObject
            services.AddBussinessObjectInjection();

            //StartUpFilter
            services.AddTransient<IStartupFilter, MigrateStartupFilter>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            if (bool.TryParse(Configuration["IsEnableSwagger"],out var isEnableSwagger) && isEnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/BusinessService/swagger.json", "Jay.NC.Workflow.WebApi Business Service");
                    c.SwaggerEndpoint("/swagger/UtilService/swagger.json", "Jay.NC.Workflow.WebApi Util Service");
                });
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            EnumHelper.InitEnumItemDtosDic("Jay.NC.Workflow.WebApi.Model");
            EnumHelper.InitEnumItemDtosDic("Jay.NC.Workflow.WebApi.Common");
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
