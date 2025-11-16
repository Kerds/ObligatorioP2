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

        public IActionResult Index()
        {
            return View();
        }
        
        
        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult Gastos()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.Succes = TempData["Succes"];

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
            IEnumerable<Usuario> miembrosEquipo =  null;
            try
            {
                miembrosEquipo = usuario.GetMiembrosEquipo();
                ViewBag.MiembrosEquipo = miembrosEquipo;
                ViewBag.Pagos = sistema.GetPagosUsuario(usuario);
                ViewBag.DtoUser = usuario.GetDtoUser();
                ViewBag.DtoUser.totalMes = sistema.GetTotalPagosUsuario(usuario);
            }
            catch(Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.MiembrosEquipo = new List<Usuario>();
                ViewBag.Pagos = new List<Pago>();
                ViewBag.DtoUser = new { };
            }
            return View(usuario);
        }
        
        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult Pagos()
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

            IEnumerable<Usuario> miembrosEquipo = null;
            IEnumerable<Pago> pagosEquipo = new List<Pago>();
            try
            {
                pagosEquipo = sistema.GetPagosMiembrosEquipo(usuario.GetNombreEquipo(), usuario);
                ViewBag.MisPagos = sistema.GetPagosUsuario(usuario);
                ViewBag.PagosEquipo = pagosEquipo;

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                ViewBag.MisPagos = new List<Pago>();
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
                TempData["Succes"] = "Tipo de gasto creado con exito";

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
            
            if (tipoGasto == null)
            {
                return View(tipoGasto);
            }
            try
            {
                sistema.TipoGastoTienePago(tipoGasto);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("Gastos");
            }
            return View(tipoGasto);
        }

        [HttpPost]

        public IActionResult EliminarGasto(TipoGasto tg)
        {
            TipoGasto tipoGasto = sistema.GetTipoGasto(tg.Id);
            sistema.BajaGasto(tipoGasto);
            TempData["Succes"] = "Tipo de gasto eliminado con exito";

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