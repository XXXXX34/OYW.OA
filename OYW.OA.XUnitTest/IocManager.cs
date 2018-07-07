using Autofac;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using OYW.OA.DTO;
using OYW.OA.EFRepositories;
using OYW.OA.Infrastructure.Aop;
using OYW.OA.Infrastructure.Redis;
using OYW.OA.Web.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OYW.OA.XUnitTest
{
    public class IocManager
    {

        private static IocManager _instance;

        public static IocManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IocManager();
                    _instance.Init();
                }
                return _instance;
            }
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private IContainer Container { get; set; }
        public void Init()
        {
            var builder = new ContainerBuilder();
            RegisterService(builder);
            Container = builder.Build();
        }

        private void RegisterService(ContainerBuilder builder)
        {

            DbContextOptionsBuilder<OAEntity> dbContextOptionsBuilder = new DbContextOptionsBuilder<OAEntity>();
            dbContextOptionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OYW.OA;Integrated Security=True");
            builder.Register<OAEntity>(u => new OAEntity(dbContextOptionsBuilder.Options)).InstancePerDependency();

            builder.RegisterType(typeof(UserMgr)).AsSelf().SingleInstance();
            builder.RegisterType(typeof(RedisHelper)).AsSelf().SingleInstance();

            builder.RegisterType<AopInterceptor>();
            var applicationServices = Assembly.Load("OYW.OA.Application");
            builder.RegisterAssemblyTypes(applicationServices)
           .Where(t => t.Name.EndsWith("Service") && !t.GetTypeInfo().IsAbstract)
           .AsImplementedInterfaces().InstancePerDependency();

            builder.Register<OAUser>(u => GetCurrentUser()).InstancePerDependency();
            builder.Register<ILog>(u => GetLog()).InstancePerDependency();
        }

        private OAUser GetCurrentUser()
        {
            string sessionid = "";
            //var redisHelper = IocManager.Resolve<RedisHelper>();
            //var user = redisHelper.Get<OAUser>(sessionid);
            //if (user == null) user = new OAUser { };
            return null;
        }

        private ILog GetLog()
        {
            return null;
        }
    }
}
