using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;

namespace FeedbackService.Application.Queries.GetFeedbackById;

public class GetFeedbackByIdQueryHandler(IUnitOfWork unitOfWork) : QueryHandler<GetFeedbackByIdQuery, GetFeedbackByIdQueryResult>
{
    #region Variables
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #endregion

    #region Handle
    public override Task<Response<GetFeedbackByIdQueryResult>> HandleAsync(GetFeedbackByIdQuery query)
     => ServiceHandlerAsync(async (qry) =>
     {

         //Return Feedback
         var feedback = await _unitOfWork.Feedback.GetFeedbackById(query.Id);


         return new Response<GetFeedbackByIdQueryResult>
         {
             Success = true,
             Data = new GetFeedbackByIdQueryResult
             {
                 Id = feedback.Id,
                 CustomerName = feedback.CustomerName,
                 CategoryId = feedback.CategoryId,
                 CategoryName = feedback.CategoryName,
                 Description = feedback.Description,
                 SubmissionDate = feedback.SubmissionDate
             },
             Message = "Returning the feedback successfully"
         };
     }, query);
    #endregion
}
