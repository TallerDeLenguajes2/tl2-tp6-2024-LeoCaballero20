using System.ComponentModel.DataAnnotations;
public class Cliente {
    private int clienteId;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    private string nombre;

    [EmailAddress(ErrorMessage = "Formato de email incorrecto.")]
    private string email;

    [Phone(ErrorMessage = "Formato de teléfono incorrecto.")]
    private string telefono;
    public Cliente() {}
    public Cliente(int id, string nombre, string email, string telefono) {
        clienteId = id;
        this.nombre = nombre;
        this.email = email;
        this.telefono = telefono;
    }

    public int ClienteId { get => clienteId; set => clienteId = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Email { get => email; set => email = value; }
    public string Telefono { get => telefono; set => telefono = value; }
}