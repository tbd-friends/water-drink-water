using System.Security.Cryptography;
using System.Text;

namespace TbdFriends.WaterDrinkWater.Application.Values;

public class Password(string value)
{
    private string Value { get; } = value;

    public static implicit operator string(Password password)
    {
        using SHA256 sha256Hash = SHA256.Create();

        // ComputeHash - returns byte array  
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password.Value));

        // Convert byte array to a string   
        var builder = new StringBuilder();

        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }

    public static bool Compare(string password, string hash)
    {
        string hashOfInput = new Password(password);

        return hashOfInput == hash;
    }
}