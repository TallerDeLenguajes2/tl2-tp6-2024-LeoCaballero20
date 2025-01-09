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

    [HttpGet]

    public ActionResult CrearPresupuesto() {
        return View();
    }

    [HttpPost]

    public ActionResult CrearPresupuesto(CrearPresupuestoViewModel presupModelo) {
        Presupuesto ultimoPresup = new();
        if (repositorio.ListarPresupuestos().Count()!=0) {
            ultimoPresup = repositorio.ListarPresupuestos().MaxBy(x => x.IdPresupuesto);
        } else {
            ultimoPresup.IdPresupuesto = 0;
        }
        Presupuesto presup = new(ultimoPresup.IdPresupuesto+1, presupModelo.IdCliente);
        Producto prod = new ProductoRepository().ObtenerProducto(presupModelo.IdProducto);
        PresupuestoDetalle detalle = new(prod, presupModelo.Cantidad);
        presup.Detalle.Add(detalle);
        repositorio.CrearPresupuesto(presup);
        repositorio.AgregarDetallePresupuesto(presup.IdPresupuesto, prod, presupModelo.Cantidad);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("EliminarPresupuesto/{id}")]

    public ActionResult EliminarPresupuesto(int id) {
        Presupuesto pre = repositorio.ObtenerDetallePresupuesto(id);
        return View(pre);
        
    }

    [HttpPost("EliminarPresupuesto/{presupuesto}")]

    public ActionResult EliminarPresupuesto(Presupuesto pre) {
        repositorio.EliminarPresupuesto(pre.IdPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("AgregarProducto/{id}")]

    public ActionResult AgregarProducto(int id) {
        Presupuesto pre = repositorio.ObtenerDetallePresupuesto(id);
        return View(pre);
    }

    [HttpPost("AgregarProducto/{presupuesto}")]

    public ActionResult AgregarProducto(AgregarProductoViewModel agregModel) {
        if (ModelState.IsValid) {
            Producto pro = new ProductoRepository().ObtenerProducto(agregModel.IdProducto);
            repositorio.AgregarDetallePresupuesto(agregModel.IdPresupuesto, pro, agregModel.Cantidad);
        }
        return RedirectToAction("MostrarDetallePresupuesto", new {id = agregModel.IdPresupuesto});
    }

}