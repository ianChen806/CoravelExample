using Coravel;
using Coravel.Scheduling.Schedule.Interfaces;
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
        scheduler.OnWorker("MyWorker1")
            .Schedule<ShowNowTimeJob>().EverySeconds(10);
        scheduler.OnWorker("MyWorker2")
            .Schedule<ShowNowTimeJob>().EverySeconds(30);
    })
    .LogScheduledTaskProgress(app.Services.GetService<ILogger<IScheduler>>());

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
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
