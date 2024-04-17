namespace FeedbackService.Domain.DTOs;

public class CategoryFeedbackFromSpDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int FeedbackId { get; set; }
    public string CustomerName { get; set; }
    public string Description { get; set; }
    public DateTime SubmissionDate { get; set; }
}
