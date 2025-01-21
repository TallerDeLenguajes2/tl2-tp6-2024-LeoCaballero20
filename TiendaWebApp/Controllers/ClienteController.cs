using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class ClienteController : Controller {
    private IClienteRepository _repositorio;
    public ClienteController(IClienteRepository repo) {
        _repositorio = repo;
    }

    [HttpGet]

    public ActionResult ListarClientes() {
        return View(_repositorio.ListarClientes());
    }
    
    [HttpGet]
    public ActionResult CrearCliente() {
        List<Cliente> listaClientes = _repositorio.ListarClientes();
        return View(listaClientes);
    }

    [HttpPost]
    public ActionResult CrearCliente(Cliente nuevo) {
        if (!ModelState.IsValid) return RedirectToAction("ListarClientes");
        Cliente nuevoCliente = new(nuevo.ClienteId+1,nuevo.Nombre,nuevo.Email,nuevo.Telefono);
        _repositorio.CrearCliente(nuevoCliente);
        return RedirectToAction("ListarClientes");
    }

    [HttpGet("EliminarCliente/{id}")]

    public ActionResult EliminarCliente(int id) {
        Cliente cliente = _repositorio.ObtenerCliente(id);
        return View(cliente);
    }

    [HttpPost("EliminarCliente/{cliente}")]

    public ActionResult EliminarCliente([FromForm]Cliente cliente) {
        _repositorio.EliminarCliente(cliente.ClienteId);
        return RedirectToAction("ListarClientes");
    }
}