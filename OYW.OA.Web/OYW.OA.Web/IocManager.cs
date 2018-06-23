using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OYW.OA.Web
{
    public class IocManager
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static T Resolve<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }
}
