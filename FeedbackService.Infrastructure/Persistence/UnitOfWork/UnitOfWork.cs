using FeedbackService.Domain.Repositories;

namespace FeedbackService.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(ICategoryRepository category, IFeedbackRepository feedback) : IUnitOfWork
{
    public ICategoryRepository Category { get; } = category;

    public IFeedbackRepository Feedback { get; } = feedback;
}
