using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class PresupuestoController : Controller {
    private IPresupuestoRepository _repositorioPresupuesto;

    private IProductoRepository _repositorioProducto;
    private readonly ILogger<PresupuestoController> _logger;
    public PresupuestoController(IPresupuestoRepository repo, IProductoRepository repoPro, ILogger<PresupuestoController> logger) {
        _repositorioPresupuesto = repo;
        _repositorioProducto = repoPro;
        _logger = logger;
    }

    [HttpGet]

    public ActionResult ListarPresupuestos() {
        try {
            ViewData["AccessLevels"] = HttpContext.Session.GetString("AccessLevels");
            return View(_repositorioPresupuesto.ListarPresupuestos());
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
            Presupuesto p = _repositorioPresupuesto.ObtenerDetallePresupuesto(id);
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
            if (_repositorioPresupuesto.ListarPresupuestos().Count()!=0) {
                ultimoPresup = _repositorioPresupuesto.ListarPresupuestos().MaxBy(x => x.IdPresupuesto);
            } else {
                ultimoPresup.IdPresupuesto = 0;
            }
            Presupuesto presup = new(ultimoPresup.IdPresupuesto+1, presupModelo.IdCliente);
            Producto prod = _repositorioProducto.ObtenerProducto(presupModelo.IdProducto);
            PresupuestoDetalle detalle = new(prod, presupModelo.Cantidad);
            presup.Detalle.Add(detalle);
            _repositorioPresupuesto.CrearPresupuesto(presup);
            _repositorioPresupuesto.AgregarDetallePresupuesto(presup.IdPresupuesto, prod, presupModelo.Cantidad);
            return RedirectToAction("ListarPresupuestos");
        }
        catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
        
        
    }

    [HttpGet("EliminarPresupuesto/{id}")]

    public ActionResult EliminarPresupuesto(int id) {
        Presupuesto pre = _repositorioPresupuesto.ObtenerDetallePresupuesto(id);
        return View(pre);
        
    }

    [HttpPost("EliminarPresupuesto/{presupuesto}")]

    public ActionResult EliminarPresupuesto(Presupuesto pre) {
        _repositorioPresupuesto.EliminarPresupuesto(pre.IdPresupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet("AgregarProducto/{id}")]

    public ActionResult AgregarProducto(int id) {
        Presupuesto pre = _repositorioPresupuesto.ObtenerDetallePresupuesto(id);
        return View(pre);
    }

    [HttpPost("AgregarProducto/{presupuesto}")]

    public ActionResult AgregarProducto(AgregarProductoViewModel agregModel) {
        if (ModelState.IsValid) {
            Producto pro = _repositorioProducto.ObtenerProducto(agregModel.IdProducto);
            _repositorioPresupuesto.AgregarDetallePresupuesto(agregModel.IdPresupuesto, pro, agregModel.Cantidad);
        }
        return RedirectToAction("MostrarDetallePresupuesto", new {id = agregModel.IdPresupuesto});
    }

}