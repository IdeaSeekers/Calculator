using System.Text.Json;
using Auth;
using Database;
using Domain;

namespace ServerAPI;

using Microsoft.AspNetCore.Mvc;
using Calculator;

public class CalculationsController : Controller
{
    DatabaseAPI _dbApi = new DatabaseAPI("user", "password", "database");
    
    [HttpPost, Route("/calculate")]
    public ActionResult Calculate([FromBody] JsonElement json)
    {
        var authToken = json.GetProperty("authToken").ToString();
        var calculationRequest = json.GetProperty("calculation").GetString();

        if (string.IsNullOrEmpty(calculationRequest))
        {
            return BadRequest();
        }

        var calculationQuery = new CalculationQuery(calculationRequest);
        var calculationResult = CalculatorAPI.Calculate(calculationQuery).Result;

        if (calculationResult.IsFailed)
        {
            return Json(new { comment = calculationResult.Errors.First() });
        }

        if (string.IsNullOrEmpty(authToken))
        {
            return Json(new { result = calculationResult.Value, comment = "OK" });   
        }
        
        var authApi = new AuthAPI();
        var userInfo = authApi.Verify(new Token(authToken)).Token;

        if (userInfo.IsFailed)
        {
            return Json(new { result = calculationResult.Value, comment = "OK" });   
        }

        var calculationsHistory = _dbApi.GetHistory(userInfo.Value);
        return Json(new { history = calculationsHistory, result = calculationResult.Value, comment = "OK" });
    }
}
