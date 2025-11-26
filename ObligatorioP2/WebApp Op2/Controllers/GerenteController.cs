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
            DTOUser dtoUser = null;
            try
            {
                miembrosEquipo = usuario.GetMiembrosEquipo();

                 dtoUser = new DTOUser(usuario);
                dtoUser.totalMes = sistema.GetTotalPagosUsuario(usuario);
                dtoUser.MiembrosEquipo = miembrosEquipo;

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
               dtoUser = new DTOUser(usuario);
                dtoUser.MiembrosEquipo = new List<Usuario>();
            }

            return View(dtoUser);
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


        [LogActionFilter]
        [RolActionFilter()]
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
        [LogActionFilter]
        [RolActionFilter()]
        [HttpPost]
        public IActionResult EliminarGasto(TipoGasto tg)
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
            if(!usuario.MiRol.RemoveTipoGasto())
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
                TipoGasto tipoGasto = sistema.GetTipoGasto(tg.Id);
                sistema.BajaGasto(tipoGasto);
                TempData["Succes"] = "Tipo de gasto eliminado con exito";
            }catch(Exception e) { 
                ViewBag.Error = e.Message;
                return View();
            }
            
            return RedirectToAction("Gastos", "Gerente");
        }


        [LogActionFilter]
        [RolActionFilter()]
        public IActionResult AltaPago()
        {
            ViewBag.TiposGasto = sistema.GetTipoGastosActivos();
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
            if(!usuario.MiRol.CargarNuevoPago())
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
                sistema.AltaPagoDesdeDTO(dto, usuario);
            }catch(Exception e) { 
                ViewBag.Error = e.Message;
                return View();
            }
            return RedirectToAction("Pagos");
            
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
            DTOUser dtoUser = null;
            try
            {
                dtoUser = new DTOUser(usuario);
                pagosEquipo = sistema.GetPagosMiembrosEquipo(usuario.GetNombreEquipo(), usuario);
                dtoUser.MisPagos = sistema.GetPagosUsuario(usuario);
                dtoUser.PagosEquipo = pagosEquipo;
                ViewBag.FechaFiltro = DateTime.Now;;
            }
            catch (Exception e)
            {
                dtoUser = new DTOUser(usuario);

                ViewBag.Error = e.Message;
                dtoUser.MisPagos = new List<Pago>();
                dtoUser.PagosEquipo = new List<Pago>();
                ViewBag.FechaFiltro = DateTime.Now;;

            }
            return View(dtoUser);
        }
        [LogActionFilter]
        [RolActionFilter()]
        [HttpPost]
        public IActionResult Pagos(DateTime fecha)
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
            DTOUser dtoUser = null;
            try
            {
                dtoUser = new DTOUser(usuario);
                pagosEquipo = sistema.GetPagosMiembrosEquipo(usuario.GetNombreEquipo(), usuario, fecha);
                dtoUser.MisPagos = sistema.GetPagosUsuario(usuario, fecha);
                dtoUser.PagosEquipo = pagosEquipo;
                ViewBag.FechaFiltro = fecha;
            }
            catch (Exception e)
            {
                dtoUser = new DTOUser(usuario);

                ViewBag.Error = e.Message;
                dtoUser.MisPagos = new List<Pago>();
                dtoUser.PagosEquipo = new List<Pago>();
                ViewBag.FechaFiltro = fecha;

            }
            return View(dtoUser);
        }
        }
}