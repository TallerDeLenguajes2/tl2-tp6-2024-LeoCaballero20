public interface IClienteRepository {
    public void CrearCliente(Cliente nuevoCliente);
    public List<Cliente> ListarClientes();
    public Cliente ObtenerCliente(int id);
    public void ModificarCliente(int id, Cliente cliente);
    public void EliminarCliente(int id);

}