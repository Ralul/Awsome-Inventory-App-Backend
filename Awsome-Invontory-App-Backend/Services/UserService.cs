using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Awsome_Invontory_App_Backend.Entities;
using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Awsome_Invontory_App_Backend.Services;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly AppSettings _appSettings;
    private readonly Authenitcation _authencation;

    public UserService(DataContext dataContext, IOptions<AppSettings> appSettings,  Authenitcation authencation)
    {
        _dataContext = dataContext;
        _appSettings = appSettings.Value;
        _authencation = authencation;
    }

    public AuthenticateResponse Register(RegisterRequest request)
    {
        if (request.Password != request.PasswordConfirmation)
        {
            throw new Exception("Passwords do not match");
        }

        if (_dataContext.Users.Any(x => x.Email == request.Email))
        {
            throw new Exception("User with same email address already exists");
        }

        
        var (hash, salt) = _authencation.SaltAndHash(request.Password);
        
        var user = new User()
        {
            Email = request.Email,
            Username = request.Username,
            HashedPassword = hash,
            Salt = salt
        };
        
        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
        
        return new AuthenticateResponse(user,  GenerateJwtToken(user));
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var matchingUser = _dataContext.Users.FirstOrDefault(x => x.Email == model.Email);

        if (matchingUser == null)
        {
            throw new Exception("User not found");
        }
        
        if (Authenitcation.VerifyPassword(model.Password, matchingUser.HashedPassword, matchingUser.Salt))
        {
            return new AuthenticateResponse(matchingUser, GenerateJwtToken(matchingUser));
        }

        throw new Exception("Passwords do not match");
    }
    
    public IEnumerable<User> GetAll()
    {
        return _dataContext.Users;
    }

    public User? GetById(Guid id)
    {
        return _dataContext.Users.FirstOrDefault(x => x.Id == id);
    }

    private string GenerateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim("id", user.Id.ToString())]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}