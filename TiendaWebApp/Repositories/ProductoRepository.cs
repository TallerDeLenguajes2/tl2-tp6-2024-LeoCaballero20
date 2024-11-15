
using Microsoft.Data.Sqlite;

public class ProductoRepository : IProductoRepository
{
    private string connectionString = "Data Source=db/Tienda.db;Cache=Shared";
    private string queryString;
    public void CrearProducto(Producto producto)
    {
        queryString = "INSERT INTO Productos (idProducto, Descripcion, Precio) VALUES (@id, @descripcion, @precio);";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", producto.IdProducto);
            command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@precio", producto.Precio);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void EliminarProducto(int id)
    {
        queryString = "DELETE FROM Productos WHERE idProducto = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public List<Producto> ListarProductos()
    {
        List<Producto> listaProductos = new();
        queryString = "SELECT * FROM Productos";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    int id = reader.GetInt32(0);
                    string descripcion = reader[1].ToString();
                    int precio = reader.GetInt32(2);
                    Producto producto = new(id, descripcion, precio);
                    listaProductos.Add(producto);
                }
            }
            connection.Close();
        }
        return listaProductos;
    }

    public void ModificarProducto(int id, Producto producto)
    {
        queryString = "UPDATE Productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@precio", producto.Precio);
            command.Parameters.AddWithValue("@id", producto.IdProducto);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public Producto ObtenerProducto(int id)
    {
        Producto producto = new();
        queryString = "SELECT * FROM Productos WHERE idProducto = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    producto.IdProducto = reader.GetInt32(0);
                    producto.Descripcion = reader[1].ToString();
                    producto.Precio = reader.GetInt32(2);
                }
            }
            connection.Close();
        }
        return producto;
    }
}