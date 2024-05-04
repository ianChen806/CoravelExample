using Coravel.Queuing.Interfaces;
using CoravelExample.Apis;

namespace CoravelExample.Jobs;

internal static class WeatherEndpoints
{
    private readonly static string[] _strings = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public static void MapWeatherEndpoints(this WebApplication webApplication)
    {
        webApplication.MapGet("/weatherforecast", Index)
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }

    private static WeatherForecast[] Index(IQueue queue)
    {
        var guid = queue.QueueInvocableWithPayload<SendNotifyInvocable, NotifyPayload>(new NotifyPayload()
        {
            Message = "Hello, World!"
        });
        Console.WriteLine(guid);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _strings[Random.Shared.Next(_strings.Length)]
            ))
            .ToArray();
    }
}
