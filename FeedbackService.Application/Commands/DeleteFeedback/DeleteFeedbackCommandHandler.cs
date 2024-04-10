using FeedbackService.Application.Commands.DeleteFeedback;
using FeedbackService.Application.Extensions;
using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Application.Commands.DeleteFeedback
{
    internal class DeleteFeedbackCommandHandler(DeleteFeedbackCommandValidator validator, IUnitOfWork unitOfWork) : CommandHandler<DeleteFeedbackCommand, bool>
    {
        #region Variables
        private readonly DeleteFeedbackCommandValidator _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        #endregion

        #region Handle
        protected override Task<Response<bool>> HandleAsync(DeleteFeedbackCommand command)
        => ServiceHandlerAsync(async (cmd) =>
        {
            //Validate if the model is valid
            var validation = _validator.Validate(command);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors.ErrorsToString());

            var existingFeedback = await _unitOfWork.Feedback.GetFeedbackById(command.Id);

            //Mapping the model 
            var feedback = new Feedback
            {
                Id = existingFeedback.Id,
                CustomerName = existingFeedback.CustomerName,
                CategoryId = existingFeedback.CategoryId,
                Description = existingFeedback.Description,
                SubmissionDate = existingFeedback.SubmissionDate
            };

            //Delete the feedback
            var response = await _unitOfWork.Feedback.DeleteAsync(feedback);

            if (!response)
            {
                return new Response<bool>
                {
                    Data = response,
                    Message = "The feedback can't be deleted"
                };
            }

            return new Response<bool>
            {
                Success = true,
                Data = response,
                Message = "The feedback was deleted successfully"
            };
        }, command);
        #endregion
    }
}