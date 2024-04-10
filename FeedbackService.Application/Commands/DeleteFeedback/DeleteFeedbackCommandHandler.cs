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

            //Create a new feedback model
            var newFeedback = new Feedback()
            {
                CustomerName = command.CustomerName,
                CategoryId = command.CategoryId,
                Description = command.Description,
                SubmissionDate = DateTime.UtcNow
            };

            //Delete the feedback
            var response = await _unitOfWork.Feedback.DeleteAsync(newFeedback);

            if (!response)
            {
                return new Response<bool>
                {
                    Data = response,
                    Message = "The feedback can't be Deleteed"
                };
            }

            return new Response<bool>
            {
                Success = true,
                Data = response,
                Message = "The feedback was Deleteed successfully"
            };
        }, command);
        #endregion
    }