public class Presupuesto {
    private int idPresupuesto;
    private string nombreDestinatario;
    private List<PresupuestoDetalle> detalle;
    public Presupuesto() {}
    public Presupuesto(int id, string nombre) {
        idPresupuesto = id;
        nombreDestinatario = nombre;
        Detalle = new();
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

    public double MontoPresupuesto() {
        double montoTotal = 0;
        foreach (PresupuestoDetalle pd in Detalle) {
            montoTotal += pd.Producto.Precio * pd.Cantidad;
        }
        return montoTotal;
    }
    public double MontoPresupuestoConIva() {
        return MontoPresupuesto() * 1.21;
    }
    public int CantidadProductos() {
        int cant = 0;
        foreach (PresupuestoDetalle pd in Detalle) {
            cant += pd.Cantidad;
        }
        return cant;
    }
}