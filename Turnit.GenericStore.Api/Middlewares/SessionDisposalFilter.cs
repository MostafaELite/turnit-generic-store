using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Turnit.GenericStore.Api.Middlewares
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SessionDisposalFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var usedSession = context.HttpContext.RequestServices.GetService(typeof(NHibernate.ISession)) as NHibernate.ISession;
            usedSession.Dispose();
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
