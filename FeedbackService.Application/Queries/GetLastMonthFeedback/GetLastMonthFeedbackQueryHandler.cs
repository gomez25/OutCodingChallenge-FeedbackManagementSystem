using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;

namespace FeedbackService.Application.Queries.GetLastMonthFeedback
{
    public class GetLastMonthFeedbackQueryHandler(IUnitOfWork unitOfWork) : QueryHandler<GetLastMonthFeedbackQuery, GetLastMonthFeedbackQueryResult>
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        #endregion

        #region Handle
        public override Task<Response<GetLastMonthFeedbackQueryResult>> HandleAsync(GetLastMonthFeedbackQuery query)
        => ServiceHandlerAsync(async (qry) =>
        {

            //Return all feedback of the last month
            var lastMonthFeedbackList = await _unitOfWork.Feedback.GetLastMonthAsync();


            return new Response<GetLastMonthFeedbackQueryResult>
            {
                Success = true,
                Data = new GetLastMonthFeedbackQueryResult
                {
                    GetLastMonthFeedbackList = lastMonthFeedbackList
                },
                Message = "The feedback was updated successfully"
            };
        }, query);
        #endregion
    }
}