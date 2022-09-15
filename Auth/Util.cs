using System.Text;
using Domain;

namespace Auth;

public static class Util
{
    public static bool ValidateLogin(Login login)
    {
        var loginString = login.Data;
        return loginString.Length is >= 4 and <= 20 &&
               loginString.All(ch => char.IsLetter(ch) || char.IsDigit(ch) || ch == '_' || ch == '-');
    }

    public static bool ValidatePassword(Password password)
    {
        var passwordData = password.Data;
        return passwordData.Length is >= 8 and <= 40 &&
               passwordData.All(ch => char.IsLetter(ch) || char.IsDigit(ch) || ch == '-' || ch == '#' || ch == '_');
    }

    public static string GenerateTokenBasedOnUserAndTime(User user, DateTime date)
    {
        byte[] time = BitConverter.GetBytes(date.ToBinary());
        byte[] loginLength = BitConverter.GetBytes(user.Login.Data.Length);
        byte[] loginBytes = Encoding.ASCII.GetBytes(user.Login.Data);
        byte[] passwordLength = BitConverter.GetBytes(user.Password.Data.Length);
        byte[] passwordBytes = Encoding.ASCII.GetBytes(user.Password.Data);
        byte[] bytes = time
            .Concat(loginLength)
            .Concat(loginBytes)
            .Concat(passwordLength)
            .Concat(passwordBytes)
            .ToArray();
        string token = Convert.ToBase64String(bytes);
        return token;
    }

    public static User ParseUserFromToken(Token token)
    {
        byte[] data = Convert.FromBase64String(token.Data);

        DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

        int loginLength = BitConverter.ToInt32(data, 8);
        String loginData = new String(
            data[(8 + 4)..(8 + 4 + loginLength)]
                .Select(b => (char)b).ToArray()
        );

        int passwordLength = BitConverter.ToInt32(data, 8 + 4 + loginLength);
        String passwordData = new String(
            data[(8 + 4 + 4 + loginLength)..(8 + 4 + 4 + loginLength + passwordLength)]
                .Select(b => (char)b).ToArray()
        );

        return new User(new Login(loginData), new Password(passwordData));
    }

    public static string CreateMd5(string input)
    {
        using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes); // .NET 5 +
    }
}