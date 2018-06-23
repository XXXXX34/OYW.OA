using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace OYW.OA.Web.Models
{
    public class CoreActionFilterAttribute : TypeFilterAttribute
    {
        public CoreActionFilterAttribute(Type type) : base(type)
        {

        }
    }
}
