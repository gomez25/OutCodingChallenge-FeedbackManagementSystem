using FeedbackService.Domain.DTOs;
using FeedbackService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<bool> AddAsync(Feedback newFeedback);
        Task<Feedback> GetFeedbackById(int id);
        Task<List<FeedbackDto>> GetLastMonthAsync();
        Task<bool> UpdateAsync(Feedback existingFeedback);
    }
}
