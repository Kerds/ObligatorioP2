using Clases.Pagos;
using Clases.Usuarios;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using WebApp_Op2.Filters;

namespace WebApp_Op2.Controllers
{
    public class GerenteController : Controller
    {
        Sistema sistema = Sistema.GetSistema();

        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult Perfil()
        {
            string userLogged = HttpContext.Session.GetString("usuario");
            Console.WriteLine("Entré al perfil del Gerente");
            Usuario usuario = null;
            try { 
            usuario = sistema.GetUsuarioPorEmail(userLogged);
            }catch(Exception e)
            {
            
            }
            return View(usuario);
        }

        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult AltaGasto()
        {
            return View();
        }

        [LogActionFilter]
        [RolActionFilter()]
        [HttpPost]
        public IActionResult AltaGasto(TipoGasto tipoGasto)
        {
            return View();
        }
    }
}
