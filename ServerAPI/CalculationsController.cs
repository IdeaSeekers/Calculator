using System.Text.Json;
using Auth;
using Database;
using Domain;
using Newtonsoft.Json.Linq;

namespace ServerAPI;

using Microsoft.AspNetCore.Mvc;
using Calculator;

public class CalculationsController : Controller
{
    private readonly DatabaseAPI _db;
    private readonly IAuthApi _authApi;

    public CalculationsController(DatabaseAPI db, IAuthApi authApi)
    {
        _db = db;
        _authApi = authApi;
    }

    [HttpPost, Route("/calculate")]
    public ActionResult Calculate([FromBody] JsonElement json)
    {
        var authToken = "";
        try
        {
            authToken = json.GetProperty("authToken").ToString();
        }
        catch (Exception ignored) { }


        var calculationRequest = "";
        try
        {
            calculationRequest = json.GetProperty("calculation").GetString();
        }
        catch (Exception ignored) { }

        if (string.IsNullOrEmpty(calculationRequest))
        {
            return BadRequest();
        }

        var calculationQuery = new CalculationQuery(calculationRequest);
        var calculationResult = CalculatorAPI.Calculate(calculationQuery).Result;

        if (calculationResult.IsFailed)
        {
            return Json(new { comment = calculationResult.Errors.First().Message });
        }

        if (string.IsNullOrEmpty(authToken))
        {
            return Json(new { result = calculationResult.Value, comment = "OK" });   
        }
        
        var userInfo = _authApi.Verify(new Token(authToken)).Token;

        if (userInfo.IsFailed)
        {
            return Json(new { result = calculationResult.Value, comment = "OK" });   
        }

        var calculationsHistory = _db.GetHistory(userInfo.Value);
        return Json(new { history = calculationsHistory, result = calculationResult.Value, comment = "OK" });
    }
}
