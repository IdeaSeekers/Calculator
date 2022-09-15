using Auth;
using Domain;

namespace UnitTest;

[TestFixture]
public class AuthUtilTest
{
    [Test]
    [TestCase("user", true)]
    [TestCase("User", true)]
    [TestCase("user-1", true)]
    [TestCase("I_am_user_1337", true)]
    [TestCase("IdeaSeeker", true)]
    [TestCase("us", false)]
    [TestCase("user-#", false)]
    [TestCase("i-have-very-big-login-ok", false)]
    public void LoginTest(string loginData, bool valid)
    {
        var login = new Login(loginData);
        var actual = Util.ValidateLogin(login);
        Assert.That(actual, Is.EqualTo(valid));
    }
    
    [Test]
    [TestCase("password", true)]
    [TestCase("Password", true)]
    [TestCase("password-1", true)]
    [TestCase("password-#", true)]
    [TestCase("I_am_qwerty_1337", true)]
    [TestCase("IdeaSeeker", true)]
    [TestCase("us", false)]
    [TestCase("IdeaSek", false)]
    [TestCase("user-#", false)]
    [TestCase("i-have-very-very-very-very-very-very-very-very-big", false)]
    public void PasswordTest(string passwordData, bool valid)
    {
        var password = new Password(passwordData);
        var actual = Util.ValidatePassword(password);
        Assert.That(actual, Is.EqualTo(valid));
    }

    [Test]
    [TestCase("user", "password")]
    [TestCase("user-2", "password-2")]
    [TestCase("us", "pass")]
    public void EncodeDecodeTest(string userData, string passwordData)
    {
        var user = new User(new Login(userData), new Password(passwordData));
        var encodedString = Util.GenerateTokenBasedOnUserAndTime(user, DateTime.Now);
        var token = new Token(encodedString);
        var decodedUser = Util.ParseUserFromToken(token);
        Assert.That(decodedUser, Is.EqualTo(user));
    }
}