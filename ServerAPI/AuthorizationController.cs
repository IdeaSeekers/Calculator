using Microsoft.AspNetCore.Mvc;

namespace ServerAPI;

public class AuthorizationController : Controller
{
    public ActionResult SignIn()
    {
        return Json( new { id=1, value="new" } );
    }
    
    public ActionResult SignUp()
    {
        return Json( new { id=1, value="new" } );
    }
}