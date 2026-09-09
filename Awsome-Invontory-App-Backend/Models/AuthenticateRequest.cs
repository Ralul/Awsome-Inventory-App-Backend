using System.ComponentModel.DataAnnotations;

namespace Awsome_Invontory_App_Backend.Models;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}