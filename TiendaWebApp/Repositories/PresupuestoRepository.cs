
using Microsoft.Data.Sqlite;

public class PresupuestoRepository : IPresupuestoRepository
{
    private string connectionString = "Data Source=db/Tienda.db;Cache=Shared";
    private string queryString;
    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        queryString = "INSERT INTO Presupuestos (idPresupuesto, NombreDestinatario, FechaCreacion) VALUES (@id, @nombreDest, @fecha);";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", presupuesto.IdPresupuesto);
            command.Parameters.AddWithValue("@nombreDest", presupuesto.NombreDestinatario);
            command.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-M-d"));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void EliminarPresupuesto(int id)
    {
        queryString = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public List<Presupuesto> ListarPresupuestos()
    {
        List<Presupuesto> listaPresupuestos = new();
        queryString = "SELECT * FROM Presupuestos";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    int id = reader.GetInt32(0);
                    string nombreDestinatario = reader[1].ToString();
                    Presupuesto presupuesto = new(id, nombreDestinatario);
                    listaPresupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }
        return listaPresupuestos;
    }

    public void AgregarDetallePresupuesto(int id, Producto producto, int cant)
    {
        queryString = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPre, @idPro, @cant)";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@idPre", id);
            command.Parameters.AddWithValue("@idPro", producto.IdProducto);
            command.Parameters.AddWithValue("@cant", cant);
            command.ExecuteNonQuery();
            connection.Close();
        } 
    }

    public Presupuesto ObtenerDetallePresupuesto(int id)
    {
        Presupuesto presupuesto = new();
        List<PresupuestoDetalle> listaDetalles = new();
        queryString = "SELECT NombreDestinatario, idProducto, Cantidad, Descripcion, Precio FROM Presupuestos INNER JOIN PresupuestosDetalle USING (idPresupuesto) INNER JOIN Productos USING (idProducto) WHERE idPre = @id";
        using (SqliteConnection connection = new(connectionString)) {
            SqliteCommand command = new(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@idPre", id);
            using (SqliteDataReader reader = command.ExecuteReader()) {
                presupuesto.IdPresupuesto = id;
                presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                while (reader.Read()) {    
                    string descripProducto = reader["Descripcion"].ToString();
                    int precio = reader.GetInt32(4);
                    int idProducto = reader.GetInt32(1);
                    Producto prod = new(idProducto, descripProducto, precio);
                    PresupuestoDetalle detalle = new(prod, reader.GetInt32(2));
                    listaDetalles.Add(detalle);
                }
                presupuesto.Detalle = listaDetalles;
            }
            connection.Close();
        }
        return presupuesto;
    }
}