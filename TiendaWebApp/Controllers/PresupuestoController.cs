using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class PresupuestoController : Controller {
    private IPresupuestoRepository _repositorio;
    private readonly ILogger<PresupuestoController> _logger;
    public PresupuestoController(IPresupuestoRepository repo, ILogger<PresupuestoController> logger) {
        _repositorio = repo;
        _logger = logger;
    }

    [HttpGet]

    public ActionResult ListarPresupuestos() {
        try {
            ViewData["AccessLevels"] = HttpContext.Session.GetString("AccessLevels");
            return View(_repositorio.ListarPresupuestos());
        }
        catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return NotFound();
        } 
    }

    [HttpGet("DetallePresupuesto/{id}")]

    public ActionResult MostrarDetallePresupuesto(int id) {
        try {
            ViewData["AccessLevels"] = HttpContext.Session.GetString("AccessLevels");
            Presupuesto p = _repositorio.ObtenerDetallePresupuesto(id);
            return View(p);
        }
        catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return NotFound();
        }
        
    }

    [HttpGet]

    public ActionResult CrearPresupuesto() {
        return View();
    }

    [HttpPost]

    public ActionResult CrearPresupuesto(CrearPresupuestoViewModel presupModelo) {
        try {
            Presupuesto ultimoPresup = new();
            if (_repositorio.ListarPresupuestos().Count()!=0) {
                ultimoPresup = _repositorio.ListarPresupuestos().MaxBy(x => x.IdPresupuesto);
            } else {
                ultimoPresup.IdPresupuesto = 0;
            }
            Presupuesto presup = new(ultimoPresup.IdPresupuesto+1, presupModelo.IdCliente);
            Producto prod = new ProductoRepository().ObtenerProducto(presupModelo.IdProducto);
            PresupuestoDetalle detalle = new(prod, presupModelo.Cantidad);
            presup.Detalle.Add(detalle);
            _repositorio.CrearPresupuesto(presup);
            _repositorio.AgregarDetallePresupuesto(presup.IdPresupuesto, prod, presupModelo.Cantidad);
            return RedirectToAction("ListarPresupuestos");
        }
        catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
        
        
    }

    [HttpGet("EliminarPresupuesto/{id}")]

    public ActionResult EliminarPresupuesto(int id) {
        Presupuesto pre = _repositorio.ObtenerDetallePresupuesto(id);
        return View(pre);
        
    }

    [HttpPost("EliminarPresupuesto/{presupuesto}")]

    public ActionResult EliminarPresupuesto(Presupuesto pre) {
        _repositorio.EliminarPresupuesto(pre.IdPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("AgregarProducto/{id}")]

    public ActionResult AgregarProducto(int id) {
        Presupuesto pre = _repositorio.ObtenerDetallePresupuesto(id);
        return View(pre);
    }

    [HttpPost("AgregarProducto/{presupuesto}")]

    public ActionResult AgregarProducto(AgregarProductoViewModel agregModel) {
        if (ModelState.IsValid) {
            Producto pro = new ProductoRepository().ObtenerProducto(agregModel.IdProducto);
            _repositorio.AgregarDetallePresupuesto(agregModel.IdPresupuesto, pro, agregModel.Cantidad);
        }
        return RedirectToAction("MostrarDetallePresupuesto", new {id = agregModel.IdPresupuesto});
    }

}