using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class PresupuestoController : Controller {
    private PresupuestoRepository repositorio;

    public PresupuestoController() {
        repositorio = new();
    }

    [HttpGet]

    public ActionResult ListarPresupuestos() {
        return View(repositorio.ListarPresupuestos());
    }

    [HttpGet("DetallePresupuesto/{id}")]

    public ActionResult MostrarDetallePresupuesto(int id) {
        Presupuesto p = repositorio.ObtenerDetallePresupuesto(id);
        return View(p);
    }
}