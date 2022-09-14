using Microsoft.AspNetCore.Mvc;

namespace ServerAPI;

public class HistoryController : Controller
{
    public ActionResult GetHistory()
    {
        return Json( new { id=1, value="new" } );
    }
}