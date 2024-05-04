using Coravel;
using CoravelExample;
using CoravelExample.Jobs;
using CoravelExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScheduler();

builder.Services.AddScoped<MyService>();
builder.Services.AddTransient<ShowNowTimeJob>();

var app = builder.Build();
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule(() => Console.WriteLine($"func: {DateTime.Now}")).EverySeconds(10);
    scheduler.Schedule<ShowNowTimeJob>().EverySeconds(10);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();