using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp_Op2.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Obtener controller/action actuales
            string controller = context.RouteData.Values["controller"]?.ToString() ?? "";
            string action = context.RouteData.Values["action"]?.ToString() ?? "";

            if (string.Equals(controller, "Usuario", StringComparison.OrdinalIgnoreCase)
                && string.Equals(action, "Login", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // Si no hay usuario en sesión, redirigir al Login (UsuarioController.Login)
            string userLogged = context.HttpContext.Session.GetString("usuario");
            if (string.IsNullOrEmpty(userLogged))
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
                return;
            }

            // Logging opcional
            Console.WriteLine($"[Before] Action: {context.ActionDescriptor.DisplayName} - Usuario: {userLogged}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"[After] Action: {context.ActionDescriptor.DisplayName}");
        }
    }
}