using Microsoft.Data.Sqlite;
public class UsuarioRepository : IUsuarioRepository
{
    private string _connectionString;
    private string queryString;

    public UsuarioRepository(string connectionString) {
        _connectionString = connectionString;
    }
    public void CrearUsuario(Usuario nuevoUsuario)
    {
        queryString = "INSERT INTO Usuarios (IdUsuario, Nombre, Usuario, Contraseña, Rol) VALUES (@id, @nombre, @usu, @pass, @rol);";
        using (SqliteConnection connection = new(_connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", nuevoUsuario.IdUsuario);
            command.Parameters.AddWithValue("@nombre", nuevoUsuario.Nombre);
            command.Parameters.AddWithValue("@usu", nuevoUsuario.NombreUsuario);
            command.Parameters.AddWithValue("@pass", nuevoUsuario.Contraseña);
            command.Parameters.AddWithValue("@rol", nuevoUsuario.Rol);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void EliminarUsuario(int id)
    {
        queryString = "DELETE FROM Usuarios WHERE IdUsuario = @id";
        using (SqliteConnection connection = new(_connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Usuario> ListarUsuarios()
    {
        queryString = "SELECT * FROM Usuarios";
        List<Usuario> listaUsuarios = new();
        using (SqliteConnection connection = new(_connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    int id = reader.GetInt32(0);
                    string nombre = reader[1].ToString();
                    string usuario = reader[2].ToString();
                    string contraseña = reader[3].ToString();
                    string rol = reader[4].ToString();
                    Usuario Usuario = new(id, nombre, usuario, contraseña, rol);
                    listaUsuarios.Add(Usuario);
                }
            }
            connection.Close();
        }
        return listaUsuarios;
    }

    public void ModificarUsuario(int id, Usuario Usuario)
    {
        queryString = "UPDATE Usuarios SET Nombre = @nombre, Usuario = @usu, Contraseña = @pass, Rol = @rol WHERE IdUsuario = @id";
        using (SqliteConnection connection = new(_connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@nombre", Usuario.Nombre);
            command.Parameters.AddWithValue("@usu", Usuario.NombreUsuario);
            command.Parameters.AddWithValue("@pass", Usuario.Contraseña);
            command.Parameters.AddWithValue("@rol", Usuario.Rol);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public Usuario ObtenerUsuario(string username, string password)
    {
        Usuario Usuario = new();
        queryString = "SELECT * FROM Usuarios WHERE Usuario = @user AND Contraseña = @pass";
        using (SqliteConnection connection = new(_connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@user", username);
            command.Parameters.AddWithValue("@pass", password);
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    Usuario.IdUsuario = reader.GetInt32(0);
                    Usuario.Nombre = reader[1].ToString();
                    Usuario.NombreUsuario = reader[2].ToString();
                    Usuario.Contraseña = reader[3].ToString();
                    Usuario.Rol = reader[4].ToString();
                }
            }
            connection.Close();
        }
        return Usuario;
    }
}