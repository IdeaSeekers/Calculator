using System.Text.Json;
using Calculator;
using Domain;

namespace ServerAPI;

using Microsoft.AspNetCore.Mvc;
using Calculator;

public class CalculationsController : Controller
{
    [HttpPost, Route("/calculate")]
    public ActionResult Calculate([FromBody] JsonElement json)
    {
        var authToken = json.GetProperty("authToken");
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
        return Json( new { result=calculationResult.Value, comment = "OK" } );
    }
}