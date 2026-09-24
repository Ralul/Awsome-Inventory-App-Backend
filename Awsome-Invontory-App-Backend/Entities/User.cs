using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Awsome_Invontory_App_Backend.Entities;

[Table("users")]
public class User
{
    [Column("id")] public Guid Id { get; init; }
    [Column("email")] public required string Email { get; init; }
    [Column("username")] public required string Username { get; init; }

    [JsonIgnore] [Column("hashed_password")] public string HashedPassword { get; init; }
    [JsonIgnore] [Column("salt")] public string Salt { get; init; }
}