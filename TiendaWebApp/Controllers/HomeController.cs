using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("Username");
        ViewData["Nombre"] = HttpContext.Session.GetString("Nombre");
        ViewData["AccessLevels"] = HttpContext.Session.GetString("AccessLevels");
        if (username == null) return RedirectToAction("Index", "Login");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
