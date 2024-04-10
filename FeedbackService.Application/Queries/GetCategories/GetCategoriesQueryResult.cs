using FeedbackService.Domain.Entities;

namespace FeedbackService.Application.Queries.GetCategories;

public class GetCategoriesQueryResult
{
    public List<Category> Categories { get; set; }
}