using Coravel.Invocable;

namespace CoravelExample.Jobs;

public class ShowNowTimeJob : IInvocable
{
    public Task Invoke()
    {
        Console.WriteLine(DateTime.Now);
        return Task.CompletedTask;
    }
}
