using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Commands.AddFeedback;

public record AddFeedbackCommand : IRequest<Response<bool>>
{
    public string CustomerName { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
}