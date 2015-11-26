using HomeWork1.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeWork1.ActionFilters
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        // Create new stopwatch.
        Stopwatch stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary(new { controller = "客戶資料", action = "LogIn" })
                                        );

                //filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);

                //var controller = (客戶資料Controller)filterContext.Controller;
                //filterContext.Result = controller.RedirectToAction("LogIn");
            }            
            
            base.OnActionExecuting(filterContext);
        }

    }
}