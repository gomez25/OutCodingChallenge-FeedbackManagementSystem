using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<Response<GetCategoriesQueryResult>>
{
}