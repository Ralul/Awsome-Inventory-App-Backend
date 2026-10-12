using System.ComponentModel.DataAnnotations.Schema;

namespace Awsome_Invontory_App_Backend.Entities;

[Table("items")]
public class Item
{
    [Column("id")] public Guid Id { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("description")] public string Description { get; set; }
    [Column("bar_code")] public string BarCode { get; set; }
}