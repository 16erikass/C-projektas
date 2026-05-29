using System;
using System.Text;

public class PasswordManager
{
    private const string CharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private readonly Random _random = new Random();

    public string GenerateTargetHash(out string actualPassword)
    {

        int length = _random.Next(4, 6);
        StringBuilder password = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            password.Append(CharSet[_random.Next(CharSet.Length)]);
        }

        actualPassword = password.ToString();
        return CryptoUtils.ComputeSha256Hash(actualPassword);
    }
}