using Coravel.Pro.EntityFramework;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext, ICoravelProDbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<CoravelJobHistory> Coravel_JobHistory { get; set; }

    public DbSet<CoravelScheduledJob> Coravel_ScheduledJobs { get; set; }

    public DbSet<CoravelScheduledJobHistory> Coravel_ScheduledJobHistory { get; set; }
}
