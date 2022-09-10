using Database;
using Domain;

namespace Auth;

public class AuthAPI
{
    public AuthAPI(DatabaseAPI database)
    {
        _database = database;
    }

    private readonly DatabaseAPI _database;

    public VerifyResult Verify(Token token)
    {
        throw new NotImplementedException("Not implemented!");
    }
    
    public RegisterResult Register(User user)
    {
        throw new NotImplementedException("Not implemented!");
    }

    public SignInResult SignIn(User user)
    {
        throw new NotImplementedException("Not implemented!");
    }
}