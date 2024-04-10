using FeedbackService.Application.Extensions;
using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;

namespace FeedbackService.Application.Commands.AddFeedback;

public class AddFeedbackCommandHandler(AddFeedbackCommandValidator validator, IUnitOfWork unitOfWork) : CommandHandler<AddFeedbackCommand, bool>
{
    #region Variables
    private readonly AddFeedbackCommandValidator _validator = validator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #endregion

    #region Handle
    protected override Task<Response<bool>> HandleAsync(AddFeedbackCommand command)
    => ServiceHandlerAsync(async (cmd) =>
    {
        //Validate if the model is valid
        var validation = _validator.Validate(command);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors.ErrorsToString());

        //Create a new feedback model
        var newFeedback = new Feedback()
        {
            CustomerName = command.CustomerName,
            CategoryId = command.CategoryId,
            SubmissionDate = DateTime.UtcNow
        };

        //Add the feedback
        var response = await _unitOfWork.Feedback.AddAsync(newFeedback);

        if (!response)
        {
            return new Response<bool>
            {
                Data = response,
                Message = "The feedback can't be added"
            };
        }

        return new Response<bool>
        {
            Success = true,
            Data = response,
            Message = "The feedback was added successfully"
        };
    }, command);
    #endregion
}
