using Coravel.Events.Interfaces;
using CoravelExample.Models;

namespace CoravelExample.Notifies;

public class BlogPostCreated : IEvent
{
    public Post Post { get; }

    public BlogPostCreated(Post post)
    {
        Post = post;
    }
}
