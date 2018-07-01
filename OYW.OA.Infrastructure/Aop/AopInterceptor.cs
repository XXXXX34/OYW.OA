using Castle.DynamicProxy;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYW.OA.Infrastructure.Aop
{
    public class AopInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            string methodInfo = string.Format("正在调用方法 \"{0}\"  参数是 {1} ",
            invocation.Method.Name,
            string.Join(", ", invocation.Arguments.Select(a => JsonConvert.SerializeObject((a ?? ""))).ToArray()));

            invocation.Proceed();
            if (invocation.ReturnValue != null && invocation.ReturnValue is string)
            {
                invocation.ReturnValue += " AopInterceptor";
            }
            string methodRes = string.Format("方法执行完毕，返回结果：{0}\r\n", JsonConvert.SerializeObject(invocation.ReturnValue));

            IocManager.Resolve<ILog>().Info($"{methodInfo}\r\n{methodRes}");
        }
    }
}
