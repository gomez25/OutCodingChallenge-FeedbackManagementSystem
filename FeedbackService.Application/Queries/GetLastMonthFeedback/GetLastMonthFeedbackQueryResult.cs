using FeedbackService.Domain.DTOs;

namespace FeedbackService.Application.Queries.GetLastMonthFeedback
{
    public class GetLastMonthFeedbackQueryResult
    {
        public List<CategoryFeedbackDto> GetLastMonthFeedbackList { get; set; }
    }
}