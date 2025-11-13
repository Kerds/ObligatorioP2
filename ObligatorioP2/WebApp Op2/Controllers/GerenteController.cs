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
            return View(listaGastos);
        }

        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult Perfil()
        {
            string userLogged = HttpContext.Session.GetString("usuario");
            Console.WriteLine("Entré al perfil del Gerente");
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
                ViewBag.MiembrosEquipo = (sistema.GetEquipoPorNombre(usuario.GetNombreEquipo())).GetMiembros();
                ViewBag.Pagos = sistema.GetPagosUsuario(usuario);

            }catch(Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.MiembrosEquipo = new List<Usuario>();
                ViewBag.Pagos = new List<Pago>();
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
        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult AltaPago()
        {
            ViewBag.TiposGasto = sistema.GetTipoGastos();
            ViewBag.MetodosPago = MetodosPago.GetValues(typeof(MetodosPago)).Cast<MetodosPago>();
            return View();
        }
        [LogActionFilter]
        [RolActionFilter()]
        [HttpPost]
        public IActionResult AltaPago(string metodosPago, TipoGasto tipoGasto, string descripcion, DateTime fechaInicio, DateTime? fechaFin)// hacer todos los datos por separado y posteriormente crear el objeto pago
        {
            Pago pago = new Pago();
            pago.MetodosPago = (MetodosPago)Enum.Parse(typeof(MetodosPago), metodosPago);
            pago.TipoGasto = tipoGasto;
            pago.Descripcion = descripcion;
            string userLogged = HttpContext.Session.GetString("usuario");
            Usuario usuario = sistema.GetUsuarioPorEmail(userLogged);

            {
                try
                {
                    pago.Validar();
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
                //Posiblemente sustituir por el list de pagos
                return RedirectToAction("Perfil");
            }
        }
    }
}