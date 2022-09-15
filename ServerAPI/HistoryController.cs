using System.Text.Json;
using Auth;
using Database;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI;

public class HistoryController : Controller
{
    DatabaseAPI _dbApi = new DatabaseAPI("user", "password", "database");
    
    [HttpPost("/history")]
    public ActionResult GetHistory([FromBody] JsonElement json)
    {
        var authToken = "";
        try
        {
            authToken = json.GetProperty("authToken").ToString();
        }
        catch (Exception ignored) { }

        if (string.IsNullOrEmpty(authToken))
        {
            return Forbid();
        }
        var authApi = new AuthApiImpl(_dbApi);
        var userInfo = authApi.Verify(new Token(authToken)).Token;

        if (userInfo.IsFailed)
        {
            return Forbid();
        }

        var calculationsHistory = _dbApi.GetHistory(userInfo.Value);
        return Json(new { history = calculationsHistory });
    }
}
