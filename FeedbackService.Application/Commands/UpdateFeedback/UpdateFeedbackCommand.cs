using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Commands.UpdateFeedbackCommand;

public record UpdateFeedbackCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
}