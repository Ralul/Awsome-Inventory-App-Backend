using System.ComponentModel.DataAnnotations;

namespace Awsome_Invontory_App_Backend.Models;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}