using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class ProductoController : Controller 
{
    private IProductoRepository _repositorio;

    public ProductoController(IProductoRepository repo) {
        _repositorio = repo;
    }

    [HttpGet]

    public ActionResult ListarProductos() {
        return View(_repositorio.ListarProductos());
    }

    [HttpGet]

    public ActionResult CrearProducto() {
        return View();
    }

    [HttpPost]

    public ActionResult CrearProducto(string descripcion, int precio) {
        if (ModelState.IsValid) {
            Producto p = _repositorio.ListarProductos().MaxBy(x => x.IdProducto);
            Producto producto = new(p.IdProducto+1,descripcion,precio);
            _repositorio.CrearProducto(producto);
        }
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("ModificarProducto/{id}")]

    public ActionResult ModificarProducto(int id) {
        Producto p = _repositorio.ObtenerProducto(id);
        return View(p);
    }

    [HttpPost("ModificarProducto/{producto}")]

    public ActionResult ModificarProducto(Producto p) {
        _repositorio.ModificarProducto(p.IdProducto, p);
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("EliminarProducto/{id}")]

    public ActionResult EliminarProducto(int id) {
        Producto p = _repositorio.ObtenerProducto(id);
        return View(p);
    }

    [HttpPost("EliminarProducto/{producto}")]

    public ActionResult EliminarProducto(Producto p) {
        _repositorio.EliminarProducto(p.IdProducto);
        return RedirectToAction("ListarProductos");
    }


}