public interface IUsuarioRepository {
    public void CrearUsuario(Usuario nuevoUsuario);
    public List<Usuario> ListarUsuarios();
    public Usuario ObtenerUsuario(string username, string password);
    public void ModificarUsuario(int id, Usuario Usuario);
    public void EliminarUsuario(int id);

}