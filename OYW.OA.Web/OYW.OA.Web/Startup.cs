using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using OYW.OA.Application.Settings;
using OYW.OA.DTO;
using OYW.OA.DTO.Redis;
using OYW.OA.EFRepositories;
using OYW.OA.Infrastructure.Redis;
using OYW.OA.Web.Models;

namespace OYW.OA.Web
{
    public class Startup
    {
        public static ILoggerRepository loggerRepository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            loggerRepository = LogManager.CreateRepository("OYW.OA.Web");
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<OAEntity>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OASqlServer"), b => b.UseRowNumberForPaging())//UseRowNumberForPaging:兼容2008数据库
            );
            services.AddOptions();
            services.AddHttpContextAccessorSelfDefine();

            services.Configure<RedisOption>(Configuration.GetSection("RedisOption"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver())
                                          .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ContainerBuilder();
            RegisterService(builder);
            builder.Populate(services);
            var serviceProvider = new AutofacServiceProvider(builder.Build());
            IocManager.ServiceProvider = serviceProvider;
            return serviceProvider;
        }
        private void RegisterService(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(OAEntity)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UserMgr)).AsSelf().SingleInstance();
            builder.RegisterType(typeof(RedisHelper)).AsSelf().SingleInstance();
            var applicationServices = Assembly.Load("OYW.OA.Application");
            builder.RegisterAssemblyTypes(applicationServices)
           .Where(t => t.Name.EndsWith("Service"))
           .AsSelf().InstancePerLifetimeScope();
            builder.Register<OAUser>(u => GetCurrentUser()).InstancePerLifetimeScope();
            builder.Register<ILog>(u => GetLog()).InstancePerLifetimeScope();
        }

        private OAUser GetCurrentUser()
        {
            var accessor = IocManager.Resolve<IHttpContextAccessor>();
            string sessionid = "";
            accessor.HttpContext.Request.Cookies.TryGetValue("oa.sessionid", out sessionid);
            var redisHelper = IocManager.Resolve<RedisHelper>();
            var user = redisHelper.Get<OAUser>(sessionid);
            if (user == null) user = new OAUser { };
            return user;
        }

        private ILog GetLog()
        {
            var log = LogManager.GetLogger(loggerRepository.Name, typeof(Startup));
            return log;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticHttpContext();

            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var ex = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
                    ILog log = IocManager.Resolve<ILog>();
                    var msg = ex == null ? "发生错误。" : ex.Message;
                    log.Error(msg, ex);
                    context.Response.ContentType = "text/plain;charset=utf-8";
                    await context.Response.WriteAsync(msg);
                });
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
