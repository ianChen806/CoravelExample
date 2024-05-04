using Coravel.Events.Interfaces;
using Coravel.Queuing.Interfaces;
using CoravelExample.Jobs;
using CoravelExample.Models;
using CoravelExample.Notifies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoravelExample.Apis;

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

        webApplication.MapGet("/weatherforecast/newPost", NewPost)
            .WithName("NewPost")
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

    private static Results<Ok<string>, NotFound> NewPost(IDispatcher dispatcher)
    {
        var title = Guid.NewGuid().ToString();
        dispatcher.Broadcast(new BlogPostCreated(new Post()
        {
            Title = title
        }));
        return TypedResults.Ok(title);
    }
}
