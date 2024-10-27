using Microsoft.AspNetCore.Mvc;

namespace ObjectRanking.Controllers;

[ApiController]
[Route("[controller]")]
public class AbobaController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public AbobaController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "BibaBoba")]
    public void Get()
    {
        Console.WriteLine("BibaBoba");
    }
}