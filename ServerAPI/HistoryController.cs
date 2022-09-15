using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI;

public class HistoryController : Controller
{
    [HttpPost("/history")]
    public ActionResult GetHistory([FromBody] JsonElement json)
    {
        Console.WriteLine(json);
        return Json( new { id=1, value="new" } );
    }
}