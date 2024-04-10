using FluentValidation;

namespace FeedbackService.Application.Commands.AddFeedback;

public class AddFeedbackCommandValidator : AbstractValidator<AddFeedbackCommand>
{
    public AddFeedbackCommandValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty().NotNull();
        RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description).NotEmpty().NotNull();
    }
}