using Awsome_Invontory_App_Backend.Entities;

namespace Awsome_Invontory_App_Backend.Models;

public class ItemsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }

    public ItemsResponse(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Description = item.Description;
        BarCode = item.BarCode;
        
    }
}