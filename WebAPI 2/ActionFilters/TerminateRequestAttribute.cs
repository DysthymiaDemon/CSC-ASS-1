using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI_2.ActionFilters
{
    public class TerminateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //Pretend we're having trouble connecting...
            Thread.Sleep(5000);

            //And now act like we can't connect at all. 
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Close();

            throw new InvalidOperationException("TerminateRequestAttribute applied!");
        }
    }
}