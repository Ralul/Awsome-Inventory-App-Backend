using Awsome_Invontory_App_Backend.Entities;
using Awsome_Invontory_App_Backend.Models;

namespace Awsome_Invontory_App_Backend.Services;

public interface IUserService
{
    AuthenticateResponse Register(RegisterRequest request);
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User? GetById(Guid id);
}