﻿using Coravel.Invocable;
using CoravelExample.Jobs;

namespace CoravelExample.Apis;

internal class SendNotifyInvocable : IInvocable, IInvocableWithPayload<NotifyPayload>
{
    public NotifyPayload Payload { get; set; }

    public Task Invoke()
    {
        Console.WriteLine(Payload.Message);
        return Task.CompletedTask;
    }
}
