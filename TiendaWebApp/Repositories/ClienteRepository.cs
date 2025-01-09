using Microsoft.Data.Sqlite;
public class ClienteRepository : IClienteRepository
{
    private string connectionString = "Data Source=db/Tienda.db;Cache=Shared";
    private string queryString;
    public void CrearCliente(Cliente nuevoCliente)
    {
        queryString = "INSERT INTO Clientes (ClienteId, Nombre, Email, Telefono) VALUES (@id, @nombre, @email, @tel);";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", nuevoCliente.ClienteId);
            command.Parameters.AddWithValue("@nombre", nuevoCliente.Nombre);
            command.Parameters.AddWithValue("@email", nuevoCliente.Email);
            command.Parameters.AddWithValue("@tel", nuevoCliente.Telefono);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void EliminarCliente(int id)
    {
        queryString = "DELETE FROM Clientes WHERE ClienteId = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Cliente> ListarClientes()
    {
        queryString = "SELECT * FROM Clientes";
        List<Cliente> listaClientes = new();
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    int id = reader.GetInt32(0);
                    string nombre = reader[1].ToString();
                    string email = reader[2].ToString();
                    string telefono = reader[3].ToString();
                    Cliente cliente = new(id, nombre, email, telefono);
                    listaClientes.Add(cliente);
                }
            }
            connection.Close();
        }
        return listaClientes;
    }

    public void ModificarCliente(int id, Cliente cliente)
    {
        queryString = "UPDATE Clientes SET Nombre = @nombre, Email = @email, Telefono = @tel WHERE ClienteId = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@email", cliente.Email);
            command.Parameters.AddWithValue("@tel", cliente.Telefono);
            command.Parameters.AddWithValue("@id", cliente.ClienteId);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public Cliente ObtenerCliente(int id)
    {
        Cliente cliente = new();
        queryString = "SELECT * FROM Clientes WHERE ClienteId = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    cliente.ClienteId = reader.GetInt32(0);
                    cliente.Nombre = reader[1].ToString();
                    cliente.Email = reader[2].ToString();
                    cliente.Telefono = reader[3].ToString();
                }
            }
            connection.Close();
        }
        return cliente;
    }
}