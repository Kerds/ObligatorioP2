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
            Usuario usuario = sistema.GetUsuarioPorEmail(userLogged);

            return View(usuario);
        }

        public IActionResult AltaGasto()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AltaGasto(TipoGasto tipoGasto)
        {
            return View();
        }
    }
}
