using Clases.Pagos;
using Clases.Usuarios;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using WebApp_Op2.Filters;

namespace WebApp_Op2.Controllers
{
    public class EmpleadoController : Controller
    {
        Sistema sistema = Sistema.GetSistema();
        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult Perfil()
        {

            string userLogged = HttpContext.Session.GetString("usuario");
            Console.WriteLine("Entré al perfil de Empleado");
            Usuario usuario = null;
            try
            {
                usuario = sistema.GetUsuarioPorEmail(userLogged);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Usuario");
            }
            try
            {
                ViewBag.Pagos = sistema.GetPagosUsuario(usuario);
                ViewBag.DtoUser = usuario.GetDtoUser();
                ViewBag.DtoUser.totalMes = sistema.GetTotalPagosUsuario(usuario);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Pagos = new List<Pago>();
            }
            return View(usuario);
        }
        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult MisPagos()
        {
            string userLogged = HttpContext.Session.GetString("usuario");
            Usuario usuario = null;
            try
            {
                usuario = sistema.GetUsuarioPorEmail(userLogged);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Usuario");
            }
            try
            {

                ViewBag.Pagos = sistema.GetPagosUsuario(usuario);

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.Pagos = new List<Pago>();
            }
            return View(usuario);
        }

        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult AltaPago()
        {
            ViewBag.TiposGasto = sistema.GetTipoGastos();
            return View();
        }
        [LogActionFilter]
        [RolActionFilter()]
        [HttpPost]
        public IActionResult AltaPago(DTOpago dto)
        {
            string userLogged = HttpContext.Session.GetString("usuario");
            Usuario usuario = null;
            try
            {
                usuario = sistema.GetUsuarioPorEmail(userLogged);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Usuario");
            }
            try
            {
                dto.Validar();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            if (!usuario.MiRol.CargarNuevoPago())
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sistema.AltaPagoDesdeDTO(dto, usuario);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
            return RedirectToAction("Pagos");

        }
    }
}
