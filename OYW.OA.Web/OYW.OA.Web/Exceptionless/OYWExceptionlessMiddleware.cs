using Exceptionless;
using Exceptionless.Plugins;
using Microsoft.AspNetCore.Http;
using OYW.OA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OYW.OA.Web.Exceptionless
{
    public class OYWExceptionlessMiddleware
    {
        private readonly ExceptionlessClient _client;
        private readonly RequestDelegate _next;

        public OYWExceptionlessMiddleware(RequestDelegate next, ExceptionlessClient client)
        {
            _client = client ?? ExceptionlessClient.Default;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var contextData = new ContextData();
                contextData.MarkAsUnhandledError();
                contextData.SetSubmissionMethod(nameof(OYWExceptionlessMiddleware));
                if (!ex.GetType().IsSubclassOf(typeof(OYWException)))
                {
                    ex.ToExceptionless(contextData, _client).SetHttpContext(context).Submit();
                }
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync(ex.Message);
            }

            if (context.Response?.StatusCode == 404)
            {
                string path = context.Request.Path.HasValue ? context.Request.Path.Value : "/";
                _client.CreateNotFound(path).SetHttpContext(context).Submit();
            }
        }
    }
}
