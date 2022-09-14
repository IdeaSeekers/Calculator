namespace ServerAPI;

using Microsoft.AspNetCore.Mvc;

public class CalculationsController : Controller
{
    public ActionResult Calculate()
    {
        return Json( new { id=1, value="new" } );
    }
}