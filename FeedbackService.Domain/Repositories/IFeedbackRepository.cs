using FeedbackService.Domain.DTOs;
using FeedbackService.Domain.Entities;

namespace FeedbackService.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<bool> AddAsync(Feedback newFeedback);
        Task<bool> DeleteAsync(int feedbackId);
        Task<FeedbackDto> GetFeedbackById(int id);
        Task<IEnumerable<CategoryFeedbackDto>> GetLastMonthAsync();
        Task<bool> UpdateAsync(Feedback existingFeedback);
    }
}