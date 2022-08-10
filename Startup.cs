using Hospital.Database;
using Hospital.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // 注入IConfiguration的服务依赖
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Authentication:Issuer"], // 验证token的发送者，这段表示只有后端fakexiecheng.com发出的token才能被接受

                        ValidateAudience = true,
                        ValidAudience = Configuration["Authentication:Audience"], // 验证token的持有者

                        ValidateLifetime = true, // 验证token是否过期

                        IssuerSigningKey = new SymmetricSecurityKey(secretByte) // 将token的私钥从配置文件中传入进来进行加密
                    };
                });
            services.AddControllers(setupAction =>
            {
                // postman中的header
                setupAction.ReturnHttpNotAcceptable = true; // 配置支持多种返回类型，并在返回类型返回类型
            })
            .AddNewtonsoftJson(setupAction => // 这一段都是为patch而服务的，直接抄就行
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters(); // 支持返回xml
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IAffairsRepository, AffairsRepository>();


            services.AddDbContext<AppDbContext>(option => {
                option.UseOracle(Configuration["DbContext:ConnectionString"]);
                // option.UseOracle("User Id=SYSTEM;Password=Cjh010315;Data Source=localhost/orcl;");
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // 注入automapper服务依赖
            // 扫描profile文件
            // automapper会自动扫描Profiles文件夹下的所有文件，在构建函数中，完成映射关系的配置
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication(); // 你是谁

            app.UseAuthorization(); // 你可以干什么，你有什么权限
            // 注意各种服务启动的顺序！！！

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
