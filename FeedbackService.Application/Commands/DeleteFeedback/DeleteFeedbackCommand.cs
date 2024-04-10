using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Commands.DeleteFeedback;

public class DeleteFeedbackCommand : IRequest<Response<bool>>
{
    public int Id { get; set; }
}