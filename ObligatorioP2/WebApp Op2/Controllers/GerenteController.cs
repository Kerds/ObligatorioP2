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
        
        public IActionResult Gastos()
        {
            IEnumerable<TipoGasto> listaGastos = sistema.GetTipoGastos(); 
            return  View(listaGastos);
        }

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
            try
            {
                sistema.AltaTipoGasto(tipoGasto);
                return RedirectToAction("Gastos", "Gerente");
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            
            return View();
        }

        public IActionResult EliminarGasto(int id)
        {
            TipoGasto tipoGasto = sistema.GetTipoGasto(id);
            if (tipoGasto != null)
            {
                return View(tipoGasto);
            }
         return View();   
        }

        [HttpPost]

        public IActionResult EliminarGasto(TipoGasto tg)
        {
            TipoGasto tipoGasto = sistema.GetTipoGasto(tg.Id);
           sistema.BajaGasto(tipoGasto);
                
           return RedirectToAction("Gastos", "Gerente");
        }
    }
}
