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

        // ע��IConfiguration�ķ�������
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
                        ValidIssuer = Configuration["Authentication:Issuer"], // ��֤token�ķ����ߣ���α�ʾֻ�к��fakexiecheng.com������token���ܱ�����

                        ValidateAudience = true,
                        ValidAudience = Configuration["Authentication:Audience"], // ��֤token�ĳ�����

                        ValidateLifetime = true, // ��֤token�Ƿ����

                        IssuerSigningKey = new SymmetricSecurityKey(secretByte) // ��token��˽Կ�������ļ��д���������м���
                    };
                });
            services.AddControllers(setupAction =>
            {
                // postman�е�header
                setupAction.ReturnHttpNotAcceptable = true; // ����֧�ֶ��ַ������ͣ����ڷ������ͷ�������
            })
            .AddNewtonsoftJson(setupAction => // ��һ�ζ���Ϊpatch������ģ�ֱ�ӳ�����
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters(); // ֧�ַ���xml
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();


            services.AddDbContext<AppDbContext>(option => {
                option.UseOracle(Configuration["DbContext:ConnectionString"]);
                // option.UseOracle("User Id=SYSTEM;Password=Cjh010315;Data Source=localhost/orcl;");
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // ע��automapper��������
            // ɨ��profile�ļ�
            // automapper���Զ�ɨ��Profiles�ļ����µ������ļ����ڹ��������У����ӳ���ϵ������
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication(); // ����˭

            app.UseAuthorization(); // ����Ը�ʲô������ʲôȨ��
            // ע����ַ���������˳�򣡣���

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
