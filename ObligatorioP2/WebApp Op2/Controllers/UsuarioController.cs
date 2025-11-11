using Clases.Usuarios;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;

namespace WebApp_Op2.Controllers;

public class UsuarioController : Controller
{
    Sistema sistema = Sistema.GetSistema();
    
    public IActionResult Login()
    {
        
        return View();
    }
    [HttpPost]
    public IActionResult Login(string email, string contrasena)
    {
        Usuario usuario = sistema.ObtenerUsuario(email, contrasena);
        if (usuario != null)
        {
            HttpContext.Session.SetString("usuario", usuario.Email);
            if (usuario.MiRol != null) { 
                HttpContext.Session.SetString("rol", usuario.MiRol.ToString());
            }
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
            return View();
        }
    }
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    }