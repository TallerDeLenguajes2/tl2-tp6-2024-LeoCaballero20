public interface IPresupuestoRepository {
    public void CrearPresupuesto(Presupuesto presupuesto);
    public List<Presupuesto> ListarPresupuestos();
    public Presupuesto ObtenerDetallePresupuesto(int id);
    public void AgregarDetallePresupuesto(int id, Producto p, int c);
    public void EliminarPresupuesto(int id);

}