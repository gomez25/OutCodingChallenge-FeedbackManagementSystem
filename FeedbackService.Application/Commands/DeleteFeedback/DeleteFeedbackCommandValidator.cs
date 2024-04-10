using FeedbackService.Application.Commands.AddFeedback;
using FluentValidation;

namespace FeedbackService.Application.Commands.DeleteFeedback;

public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
{
    public DeleteFeedbackCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
    }
}