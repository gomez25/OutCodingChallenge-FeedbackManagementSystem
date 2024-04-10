using FeedbackService.Domain.Entities;
using FeedbackService.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Infrastructure.Persistence.Contexts;

public class FeedbackContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Feedback> Feedbacks { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new FeedbackConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
