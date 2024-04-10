using FluentValidation;

namespace FeedbackService.Application.Commands.UpdateFeedbackCommand;

public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
{
    public UpdateFeedbackCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.CustomerName).NotEmpty().NotNull();
        RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description).NotEmpty().NotNull();
    }
}