using Coravel;
using Coravel.Pro;
using Coravel.Scheduling.Schedule.Interfaces;
using CoravelExample.Apis;
using CoravelExample.Jobs;
using CoravelExample.Notifies;
using CoravelExample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages().AddNewtonsoftJson();

builder.Services.AddCoravelPro(typeof(MyDbContext));

builder.Services.AddScoped<MyService>();
builder.Services.AddTransient<ShowNowTimeJob>();
builder.Services.AddTransient<SendNotifyInvocable>();
builder.Services.AddTransient<SendEmailOfNewPost>();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
});

var app = builder.Build();
app.UseCoravelPro();

app.Services.ConfigureEvents()
    .Register<BlogPostCreated>()
    .Subscribe<SendEmailOfNewPost>();

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

app.UseRouting();
#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
#pragma warning restore ASP0014

app.MapWeatherEndpoints();

app.Run();
