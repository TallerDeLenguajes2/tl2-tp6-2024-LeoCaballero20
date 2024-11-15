public interface IProductoRepository {
    public void CrearProducto(Producto producto);
    public void ModificarProducto(int id, Producto producto);
    public List<Producto> ListarProductos();
    public Producto ObtenerProducto(int id);
    public void EliminarProducto(int id);

}