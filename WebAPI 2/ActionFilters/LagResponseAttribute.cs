using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI_2.ActionFilters
{
    public class LagResponseAttribute : ActionFilterAttribute
    {
        private static readonly Random Rand = new Random();

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Thread.Sleep(TimeSpan.FromSeconds(Rand.Next(3, 7)));
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}