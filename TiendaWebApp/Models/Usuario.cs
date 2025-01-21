using System.Data;

public class Usuario {
    private int idUsuario;
    private string nombre;
    private string nombreUsuario;
    private string contraseña;
    private string rol;

    public Usuario() {}
    public Usuario(int id, string nombre, string nombUsu, string contra, string rol) {
        idUsuario = id;
        this.nombre = nombre;
        this.nombreUsuario = nombUsu;
        contraseña = contra;
        this.rol = rol;
    }

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Contraseña { get => contraseña; set => contraseña = value; }
    public string Rol { get => rol; set => rol = value; }
}