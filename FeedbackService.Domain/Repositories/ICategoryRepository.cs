using FeedbackService.Domain.Entities;

namespace FeedbackService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
