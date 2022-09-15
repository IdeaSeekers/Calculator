using System.Text.Json;
using Auth;
using Database;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI;

public class HistoryController : Controller
{
    [HttpPost("/history")]
    public ActionResult GetHistory([FromBody] JsonElement json)
    {
        var authToken = json.GetProperty("authToken").ToString();
        if (string.IsNullOrEmpty(authToken))
        {
            return Forbid();
        }

        var dbApi = new DatabaseAPI();
        var authApi = new AuthAPI();
        var userInfo = authApi.Verify(new Token(authToken)).Token;

        if (userInfo.IsFailed)
        {
            return Forbid();
        }

        var calculationsHistory = dbApi.GetHistory(userInfo.Value);
        return Json(new { history = calculationsHistory });
    }
}
