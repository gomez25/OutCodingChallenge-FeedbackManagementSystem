namespace FeedbackService.Domain.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IFeedbackRepository Feedback { get; }
    }
}