using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            services.AddDbContext<OAEntity>(options => options.UseSqlServer(Configuration.GetConnectionString("OASqlServer")));
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
            builder.RegisterType(typeof(MenuService)).AsSelf().InstancePerLifetimeScope();
            builder.Register<OAUser>(u => GetCurrentUser()).InstancePerLifetimeScope();
        }

        private OAUser GetCurrentUser()
        {
            var accessor = IocManager.Resolve<IHttpContextAccessor>();
            string sessionid = "";
            accessor.HttpContext.Request.Cookies.TryGetValue("oa.sessionid", out sessionid);
            var redisHelper = IocManager.Resolve<RedisHelper>();
            return redisHelper.Get<OAUser>(sessionid);
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
