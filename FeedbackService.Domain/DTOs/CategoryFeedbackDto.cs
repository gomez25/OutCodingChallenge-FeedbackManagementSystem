using FeedbackService.Domain.Entities;

namespace FeedbackService.Domain.DTOs;

public class CategoryFeedbackDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public IEnumerable<Feedback> Feedbacks { get; set; }
}