using System.ComponentModel.DataAnnotations;

namespace Awsome_Invontory_App_Backend.Models;

public class ItemsRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string BarCode { get; set; }
}