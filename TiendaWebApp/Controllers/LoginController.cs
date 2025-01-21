using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _iUsuarioRepository;

    public LoginController(IUsuarioRepository iUsuarioRepository)
    {
        _iUsuarioRepository = iUsuarioRepository;
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
         if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Por favor ingrese su nombre de usuario y contraseña.";
            return View("Index", model);
        }
        var usuario  = _iUsuarioRepository.ObtenerUsuario(model.Username,model.Password);
        if(usuario != null){
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("Username", usuario.NombreUsuario);
            HttpContext.Session.SetString("Nombre",usuario.Nombre);
            HttpContext.Session.SetString("AccessLevels", usuario.Rol);
            return RedirectToAction("Index","Home");
        }
        model.ErrorMessage = "Credenciales invalidas";
        model.IsAuthenticated = false;
        return View("Index",model);
    }
    public IActionResult Logout()
    {
        // Limpiar la sesión
        HttpContext.Session.Clear();

        // Redirigir a la vista de login
        return RedirectToAction("Index");
    }

}