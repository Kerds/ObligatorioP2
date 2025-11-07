using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace WebApp_Op2.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           string userLogged = context.HttpContext.Session.GetString("usuario");
            if (userLogged != null)
            {
                context.Result = new RedirectResult("/Home/Index"); 

                return;
            }
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result == null)
            {
                string userLogged = context.HttpContext.Session.GetString("usuario");
                if (userLogged != null)
                {
                    context.Result = new RedirectResult("/Home/Index");
                    return;
                }
            }
            base.OnActionExecuted(context);
        }
    }
}