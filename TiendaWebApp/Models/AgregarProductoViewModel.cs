public class AgregarProductoViewModel {
    private int idPresupuesto;
    private int idProducto;
    private int cantidad;
    public AgregarProductoViewModel() {}

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}