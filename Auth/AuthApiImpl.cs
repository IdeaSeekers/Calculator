using Database;
using Domain;
using FluentResults;

namespace Auth;

public class AuthApiImpl : IAuthApi
{
    private readonly DatabaseAPI _db;
    public AuthApiImpl(DatabaseAPI database)
    {
        _db = database;
    }

    public VerifyResult Verify(Token token)
    {
        User user;
        try
        {
            user = Util.ParseUserFromToken(token);
        }
        catch (Exception e)
        {
            return new VerifyResult(Result.Fail("Unsuccessful."));
        }

        if (!Util.ValidateLogin(user.Login) || !Util.ValidatePassword(user.Password))
        {
            return new VerifyResult(Result.Fail("Unsuccessful."));
        }
        
        var getUserResult = _db.GetUser(new UserInfo(user.Login)).User;
        if (getUserResult.IsFailed)
        {
            return new VerifyResult(Result.Fail("Unsuccessful."));
        }

        if (user.Password != getUserResult.Value.Password)
        {
            return new VerifyResult(Result.Fail("Unsuccessful."));
        }

        return new VerifyResult(Result.Ok(new UserInfo(user.Login)));
    }
    
    public RegisterResult Register(User user)
    {
        if (!Util.ValidateLogin(user.Login))
        {
            return new RegisterResult(Result.Fail("Bad login char sequence!"));
        }
        if (!Util.ValidatePassword(user.Password))
        {
            return new RegisterResult(Result.Fail("Bad password char sequence!"));
        }

        var (login, password) = user;

        var getUserResult = _db.GetUser(new UserInfo(login)).User;
        if (getUserResult.IsSuccess)
        {
            return new RegisterResult(Result.Fail("Already registered"));
        }

        var hashedPassword = new Password(Util.CreateMd5(password.Data));
        var hashedUser = new User(login, hashedPassword);
        
        var registerUserResult = _db.RegisterUser(hashedUser).Result;
        if (registerUserResult.IsFailed)
        {
            return new RegisterResult(registerUserResult);
        }

        return new RegisterResult(Result.Ok());
    }

    public SignInResult SignIn(User user)
    {
        if (!Util.ValidateLogin(user.Login))
        {
            return new SignInResult(Result.Fail("Bad login char sequence!"));
        }
        if (!Util.ValidatePassword(user.Password))
        {
            return new SignInResult(Result.Fail("Bad password char sequence!"));
        }
        
        var (login, password) = user;

        var getUserResult = _db.GetUser(new UserInfo(login)).User;
        if (getUserResult.IsFailed)
        {
            return new SignInResult(Result.Fail("Login or password mismatch"));
        }

        var hashedPassword = new Password(Util.CreateMd5(password.Data));

        var userPassword = getUserResult.Value.Password;

        if (!hashedPassword.Equals(userPassword))
        {
            return new SignInResult(Result.Fail("Login or password mismatch"));
        }
        
        // success

        var token = Util.GenerateTokenBasedOnUserAndTime(getUserResult.Value, DateTime.Now);

        return new SignInResult(Result.Ok(new Token(token)));
    }
}