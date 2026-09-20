using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Awsome_Invontory_App_Backend.Entities;

[Table("users")]
public class User
{
    [Column("id")] public int Id { get; set; }
    [Column("first_name")] public string FirstName { get; set; }
    [Column("last_name")] public string LastName { get; set; }
    [Column("username")] public string Username { get; set; }

    [JsonIgnore] [Column("hashed_password")] public string HashedPassword { get; set; }
    [JsonIgnore] [Column("salt")] public string Salt { get; set; }
}