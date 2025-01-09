public class CrearPresupuestoViewModel {
    private int idCliente;
    private int idProducto;
    private int cantidad;
    public CrearPresupuestoViewModel() {}
    public CrearPresupuestoViewModel(int id, int idProd, int cant) {
        idCliente = id;
        idProducto = idProd;
        cantidad = cant;
    }

    public int IdCliente { get => idCliente; set => idCliente = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}