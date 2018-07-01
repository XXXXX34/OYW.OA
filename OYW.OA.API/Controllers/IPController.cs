using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OYW.OA.API.Controllers
{
    /// <summary>
    /// IP操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IPController : ControllerBase
    {
        /// <summary>
        /// 获取IP所在地
        /// </summary>
        /// <param name="ip">127.0.0.1</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetIPLocation(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return default(string);
            }
            string url = $"http://ip.chinaz.com/{ip}";
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument htmlDocument = htmlWeb.Load(url);
            var addressNode = htmlDocument.DocumentNode.SelectNodes("//span[@class='Whwtdhalf w50-0']").LastOrDefault();
            return addressNode.InnerHtml;
        }
    }
}