using Coravel.Invocable;
using CoravelExample.Services;

namespace CoravelExample.Jobs;

public class ShowNowTimeJob(MyService service) : IInvocable
{
    public Task Invoke()
    {
        Console.WriteLine(service.GetNow());
        return Task.CompletedTask;
    }
}
