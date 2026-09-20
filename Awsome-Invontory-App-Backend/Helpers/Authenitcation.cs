using System.Security.Cryptography;
using System.Text;

namespace Awsome_Invontory_App_Backend.Helpers;

public class Authenitcation
{
    private const int KeySize = 64;
    private const int Iterations = 35000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPasword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromHexString(salt), Iterations, HashAlgorithm, KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }

    public (string hash, string salt) SaltAndHash(string password)
    {
        var hash = HashPasword(password, out var salt);
        var saltString = Convert.ToHexString(salt);
        return (hash, saltString);
    }
}