using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Awsome_Invontory_App_Backend.Entities;
using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Models;
using Microsoft.AspNetCore.Identity;
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

        
        var (hash, salt) = _authencation.SaltAndHash(request.Password);
        
        Console.WriteLine(hash);
        Console.WriteLine(salt);
        
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            HashedPassword = hash
        };
        
        return new AuthenticateResponse(user,  GenerateJwtToken(user));
        
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        // var user = _dataContext.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        var user = new User()
        {
            FirstName = "",
            LastName = "",
            Username = "",
            HashedPassword = ""
        };
        
        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }
    
    public IEnumerable<User> GetAll()
    {
        return _dataContext.Users;
    }

    public User? GetById(int id)
    {
        return _dataContext.Users.FirstOrDefault(x => x.Id == id);
    }

    // helper methods
    private byte[] CalculateSHA256(string str)
    {
        var sha256 = SHA256Managed.Create();
        byte[] hashValue;
        var objUtf8 = new UTF8Encoding();
        hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

        return hashValue;
    }

    private string GenerateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}