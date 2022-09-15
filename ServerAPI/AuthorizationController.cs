using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Auth;
using Domain;

namespace ServerAPI;

public class AuthorizationController : Controller
{
    [HttpPost, Route("/signin")]
    public ActionResult SignIn([FromBody] JsonElement json)
    {
        var userLogin = json.GetProperty("login").GetString();
        var userPassword = json.GetProperty("password").GetString();

        if (string.IsNullOrEmpty(userLogin) || string.IsNullOrEmpty(userPassword))
        {
            return Forbid();
        }

        var authApi = new AuthAPI();
        var result = authApi.SignIn(new User(
                new Login(userLogin),
                new Password(userPassword)
            )
        ).Token;

        if (result.IsFailed)
        {
            return Forbid();
        }

        return Json( new {authToken = result.Value});
    }

    [HttpPost, Route("/signup")]
    public ActionResult SignUp([FromBody] JsonElement json)
    {
        var userLogin = json.GetProperty("login").GetString();
        var userPassword = json.GetProperty("password").GetString();

        if (string.IsNullOrEmpty(userLogin) || string.IsNullOrEmpty(userPassword))
        {
            return BadRequest();
        }

        var authApi = new AuthAPI();
        var registerResult = authApi.Register(new User(
                new Login(userLogin),
                new Password(userPassword)
            )
        ).Result;

        if (registerResult.IsFailed)
        {
            return BadRequest();
        }

        return Ok();
    }
}