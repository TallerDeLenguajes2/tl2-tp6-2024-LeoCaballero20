public class LoginViewModel
{
    private string username;
    private string password;
    private string errorMessage;
    private bool isAuthenticated;

    public string Username { get => username; set => username = value; }
    public string Password { get => password; set => password = value; }
    public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    public bool IsAuthenticated { get => isAuthenticated; set => isAuthenticated = value; }
} 