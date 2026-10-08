using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Models;
using Awsome_Invontory_App_Backend.Services;

namespace Awsome_Invontory_App_Backend.Controller;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = userService.Authenticate(model);

        return Ok(response);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        var response = userService.Register(model);

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = userService.GetAll();
        return Ok(users);
    }
}