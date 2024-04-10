using FeedbackService.Application.Extensions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FeedbackService.Application.Commands.UpdateFeedbackCommand;

internal class UpdateFeedbackCommandHandler(UpdateFeedbackCommandValidator validator, IUnitOfWork unitOfWork) : CommandHandler<UpdateFeedbackCommand, bool>
{
    #region Variables
    private readonly UpdateFeedbackCommandValidator _validator = validator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #endregion

    #region Handle
    protected override Task<Response<bool>> HandleAsync(UpdateFeedbackCommand command)
    => ServiceHandlerAsync(async (cmd) =>
    {
        //Validate if the model is valid
        var validation = _validator.Validate(command);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors.ErrorsToString());

        //Return the existing feedback by id
        var existingFeedback = await _unitOfWork.Feedback.GetFeedbackById(command.Id);

        //Update the existing feedback model
        existingFeedback.CustomerName = command.CustomerName;
        existingFeedback.CategoryId = command.CategoryId;
        existingFeedback.Description = command.Description;
        existingFeedback.SubmissionDate = DateTime.UtcNow;


        //Update the feedback
        var response = await _unitOfWork.Feedback.UpdateAsync(existingFeedback);

        if (!response)
        {
            return new Response<bool>
            {
                Data = response,
                Message = "The feedback can't be updated"
            };
        }

        return new Response<bool>
        {
            Success = true,
            Data = response,
            Message = "The feedback was updated successfully"
        };
    }, command);
    #endregion
}
