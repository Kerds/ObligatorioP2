using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using WebApp_Op2.Models;

namespace WebApp_Op2.Controllers;

public class HomeController : Controller
{
    Sistema sistema = Sistema.GetSistema();

    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("usuario") != null)
        {
            ViewBag.Usuario = HttpContext.Session.GetString("usuario");
        }
        else
        {
            return RedirectToAction("Login", "Usuario");
        }
        return View();
    }
   
    public IActionResult Privacy()
    {
        return View();
    }

}