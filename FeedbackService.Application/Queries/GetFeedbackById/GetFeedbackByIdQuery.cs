using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Queries.GetFeedbackById;

public class GetFeedbackByIdQuery : IRequest<Response<GetFeedbackByIdQueryResult>>
{
    public int Id { get; set; }
}