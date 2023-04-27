using System.Security.Cryptography;
using MyToDo.Application.Abstractions.Security;

namespace MyToDo.Infrastructure.Security;

internal sealed class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private const char Delimiter = ';';
    
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName,KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string hashedPassword, string inputPassword)
    {
        var items = hashedPassword.Split(Delimiter);

        var salt = Convert.FromBase64String(items[0]);
        var hash = Convert.FromBase64String(items[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, HashAlgorithmName, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}
