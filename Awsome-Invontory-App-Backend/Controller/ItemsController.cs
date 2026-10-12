using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Awsome_Invontory_App_Backend.Controller;

[ApiController]
[Route("[controller]")]
public class ItemsController(ItemService ItemService): ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = ItemService.GetAll();
        return Ok(response);
    }
}