public class CrearPresupuestoViewModel {
    string nombreDestinatario;
    int idProducto;
    int cantidad;
    public CrearPresupuestoViewModel() {}
    public CrearPresupuestoViewModel(string nombre, int idProd, int cant) {
        nombreDestinatario = nombre;
        idProducto = idProd;
        cantidad = cant;
    }

    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}