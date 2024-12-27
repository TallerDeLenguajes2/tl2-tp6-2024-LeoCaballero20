using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class ClienteController : Controller {
    private ClienteRepository repositorio;
    public ClienteController() {
        repositorio = new();
    }
}