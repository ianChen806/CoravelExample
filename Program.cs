using Coravel;
using Coravel.Scheduling.Schedule.Interfaces;
using CoravelExample.Apis;
using CoravelExample.Jobs;
using CoravelExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScheduler();
builder.Services.AddQueue();

builder.Services.AddScoped<MyService>();
builder.Services.AddTransient<ShowNowTimeJob>();
builder.Services.AddTransient<SendNotifyInvocable>();

var app = builder.Build();

app.Services.ConfigureQueue()
    .OnError(Console.WriteLine);

app.Services
    .UseScheduler(scheduler =>
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

app.MapWeatherEndpoints();

app.Run();
