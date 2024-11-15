public class Producto {
    private int idProducto;
    private string descripcion;
    private int precio;
    public Producto() {}
    public Producto(int id, string descrip, int precio) {
        idProducto = id;
        descripcion = descrip;
        this.precio = precio;
    }

    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }
}