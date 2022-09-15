using Domain;

namespace Auth;

public interface IAuthApi
{
    public VerifyResult Verify(Token token);

    public RegisterResult Register(User user);

    public SignInResult SignIn(User user);
}
