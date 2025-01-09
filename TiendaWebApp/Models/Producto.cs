using System.ComponentModel.DataAnnotations;
public class Producto {
    private int idProducto;

    [StringLength(250, ErrorMessage = "La descripción no puede tener más de 250 caracteres.")]
    private string descripcion;

    [Required] [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser un número positivo.")]
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