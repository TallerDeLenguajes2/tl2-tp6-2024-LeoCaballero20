using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class ClienteController : Controller {
    private ClienteRepository repositorio;
    public ClienteController() {
        repositorio = new();
    }

    [HttpGet]

    public ActionResult ListarClientes() {
        return View(repositorio.ListarClientes());
    }
    
    [HttpGet]
    public ActionResult CrearCliente() {
        List<Cliente> listaClientes = repositorio.ListarClientes();
        return View(listaClientes);
    }

    [HttpPost]
    public ActionResult CrearCliente(Cliente nuevo) {
        if (!ModelState.IsValid) return RedirectToAction("ListarClientes");
        Cliente nuevoCliente = new(nuevo.ClienteId+1,nuevo.Nombre,nuevo.Email,nuevo.Telefono);
        repositorio.CrearCliente(nuevoCliente);
        return RedirectToAction("ListarClientes");
    }

    [HttpGet("EliminarCliente/{id}")]

    public ActionResult EliminarCliente(int id) {
        Cliente cliente = repositorio.ObtenerCliente(id);
        return View(cliente);
    }

    [HttpPost("EliminarCliente/{cliente}")]

    public ActionResult EliminarCliente([FromForm]Cliente cliente) {
        repositorio.EliminarCliente(cliente.ClienteId);
        return RedirectToAction("ListarClientes");
    }
}