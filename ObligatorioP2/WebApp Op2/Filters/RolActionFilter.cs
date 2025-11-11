using Clases.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ObligatorioP2;

namespace WebApp_Op2.Filters
{
    // Solución: Heredar de ActionFilterAttribute para poder sobrescribir los métodos OnActionExecuting y OnActionExecuted
    public class RolActionFilter : ActionFilterAttribute
    {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
            Sistema s = Sistema.GetSistema();
                // Obtener controller/action actuales
                var controller = context.RouteData.Values["controller"]?.ToString() ?? "";
                var action = context.RouteData.Values["action"]?.ToString() ?? "";
                
                    string userLogged = context.HttpContext.Session.GetString("usuario");
                    string roleLogged = context.HttpContext.Session.GetString("rol");
                
                    if (string.IsNullOrEmpty(userLogged) || string.IsNullOrEmpty(roleLogged))
                    {
                        context.Result = new RedirectToActionResult("Login", "Usuario", null);
                        return;
                    }

            if (controller != roleLogged)
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", null);
                return;

            }
            Usuario usuario = s.GetUsuarioPorEmail(userLogged);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
            {
                Console.WriteLine($"[After] Action: {context.ActionDescriptor.DisplayName}");
            }
    }
}
