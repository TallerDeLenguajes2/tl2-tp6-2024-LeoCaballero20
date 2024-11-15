using Microsoft.AspNetCore.Mvc;

namespace TiendaWebApp.Controllers;

public class ProductoController : Controller 
{
    private ProductoRepository repositorio;

    public ProductoController() {
        repositorio = new();
    }

    [HttpGet]

    public ActionResult ListarProductos() {
        return View(repositorio.ListarProductos());
    }

    [HttpGet]

    public ActionResult CrearProducto() {
        return View();
    }

    [HttpPost]

    public ActionResult CrearProducto(string descripcion, int precio) {
        Producto p = repositorio.ListarProductos().MaxBy(x => x.IdProducto);
        Producto producto = new(p.IdProducto+1,descripcion,precio);
        repositorio.CrearProducto(producto);
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("Modificar/{id}")]

    public ActionResult ModificarProducto(int id) {
        Producto p = repositorio.ObtenerProducto(id);
        return View(p);
    }

    [HttpPost("Modificar/{producto}")]

    public ActionResult ModificarProducto(Producto p) {
        repositorio.ModificarProducto(p.IdProducto, p);
        return RedirectToAction("ListarProductos");
    }

    [HttpGet("Eliminar/{id}")]

    public ActionResult EliminarProducto(int id) {
        Producto p = repositorio.ObtenerProducto(id);
        return View(p);
    }

    [HttpPost("Eliminar/{producto}")]

    public ActionResult EliminarProducto(Producto p) {
        repositorio.EliminarProducto(p.IdProducto);
        return RedirectToAction("ListarProductos");
    }


}