using FeedbackService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedbackService.Infrastructure.Persistence.Configuration;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerName).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.SubmissionDate).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();

        builder.HasOne(x => x.Category)
               .WithMany(c => c.Feedbacks)
               .HasForeignKey(x => x.CategoryId);

    }
}