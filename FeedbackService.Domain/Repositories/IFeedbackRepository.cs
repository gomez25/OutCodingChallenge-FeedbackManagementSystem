using FeedbackService.Domain.DTOs;
using FeedbackService.Domain.Entities;

namespace FeedbackService.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<bool> AddAsync(Feedback newFeedback);
        Task<bool> DeleteAsync(Feedback feedback);
        Task<FeedbackDto> GetFeedbackById(int id);
        Task<List<FeedbackDto>> GetLastMonthAsync();
        Task<bool> UpdateAsync(Feedback existingFeedback);
    }
}
