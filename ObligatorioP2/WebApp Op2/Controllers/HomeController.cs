using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using System.Diagnostics;
using WebApp_Op2.Filters;
using WebApp_Op2.Models;

namespace WebApp_Op2.Controllers;

public class HomeController : Controller
{
    Sistema sistema = Sistema.GetSistema();

public IActionResult Index()
    {           
        ViewBag.Usuario = HttpContext.Session.GetString("usuario");
        return View();
    }
   
    public IActionResult Privacy()
    {
        return View();
    }

}