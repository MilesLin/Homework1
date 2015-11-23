using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace HomeWork1.ActionFilters
{
    public class TimerFilterAttribute : ActionFilterAttribute
    {
        // Create new stopwatch.
        Stopwatch stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch.Reset();
            // Begin timing.
            stopwatch.Start();
            //Debug.WriteLine("ActionExcuting Start Time :" + DateTime.Now);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

	        // Stop timing.
	        stopwatch.Stop();

	        // Write result.
            Debug.WriteLine("Action Time elapsed: {0}", stopwatch.Elapsed);

            //Debug.WriteLine("OnActionExecuted Start Time :" + DateTime.Now);
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            stopwatch.Reset();
            stopwatch.Start();
            //Debug.WriteLine("OnResultExecuting Start Time :" + DateTime.Now);
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Stop timing.
            stopwatch.Stop();
            Debug.WriteLine("Result Time elapsed: {0}", stopwatch.Elapsed);
//            Debug.WriteLine("OnResultExecuted Start Time :" + DateTime.Now);
            base.OnResultExecuted(filterContext);
        }
    }
}