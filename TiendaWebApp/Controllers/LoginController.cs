using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _iUsuarioRepository;

    private readonly ILogger<LoginController> _logger;

    public LoginController(IUsuarioRepository iUsuarioRepository, ILogger<LoginController> logger)
    {
        _iUsuarioRepository = iUsuarioRepository;
        _logger = logger;
    }
    public IActionResult Index(){
        if(HttpContext.Session.GetString("IsAuthenticated") == "true"){
            return RedirectToAction("Index","Home");
        }
        var model =  new LoginViewModel
        {
            IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true" 
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model){
        try {
            var usuario  = _iUsuarioRepository.ObtenerUsuario(model.Username,model.Password);
            if (model.Username != null) {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("Username", usuario.NombreUsuario);
                HttpContext.Session.SetString("Nombre",usuario.Nombre);
                HttpContext.Session.SetString("AccessLevels", usuario.Rol);
                _logger.LogInformation("El usuario {usu} ingresó correctamente", model.Username);
            } else {
                _logger.LogError("Intento de acceso inválido. Usuario ingresado: {usu}. Contraseña ingresada: {pass}",model.Username, model.Password);
            }
            return RedirectToAction("Index","Home");
        }
        catch (Exception ex){
            _logger.LogError(ex.ToString());
            model.ErrorMessage = "Credenciales invalidas";
            model.IsAuthenticated = false;
            return View("Index",model);
        }
    }

    public IActionResult Logout()
    {
        // Limpiar la sesión
        HttpContext.Session.Clear();

        // Redirigir a la vista de login
        return RedirectToAction("Index");
    }

}