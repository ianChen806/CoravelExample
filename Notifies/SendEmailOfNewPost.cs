using Coravel.Events.Interfaces;

namespace CoravelExample.Notifies;

public class SendEmailOfNewPost : IListener<BlogPostCreated>
{
    public Task HandleAsync(BlogPostCreated broadcasted)
    {
        Console.WriteLine($"Email sent for post: {broadcasted.Post.Title}");
        return Task.CompletedTask;
    }
}
