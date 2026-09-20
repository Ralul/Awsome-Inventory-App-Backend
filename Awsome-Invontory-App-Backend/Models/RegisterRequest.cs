namespace Awsome_Invontory_App_Backend.Models;

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
}