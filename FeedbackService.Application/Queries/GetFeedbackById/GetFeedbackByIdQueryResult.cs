namespace FeedbackService.Application.Queries.GetFeedbackById;

public class GetFeedbackByIdQueryResult
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public DateTime SubmissionDate { get; set; }
}
