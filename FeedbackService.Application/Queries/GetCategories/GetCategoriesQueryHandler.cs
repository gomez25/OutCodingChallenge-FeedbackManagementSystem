using FeedbackService.Application.Queries.GetLastMonthFeedback;
using FeedbackService.Domain.Repositories;
using FeedbackService.Domain.Shared;

namespace FeedbackService.Application.Queries.GetCategories;

public class GetCategoriesQueryHandler(IUnitOfWork unitOfWork) : QueryHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
    #region Variables
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #endregion

    #region Handle
    public override Task<Response<GetCategoriesQueryResult>> HandleAsync(GetCategoriesQuery query)
     => ServiceHandlerAsync(async (qry) =>
     {

         //Return all categories
         var categories = await _unitOfWork.Category.GetAll();


         return new Response<GetCategoriesQueryResult>
         {
             Success = true,
             Data = new GetCategoriesQueryResult
             {
                 Categories = categories.ToList()
             },
             Message = "Returning all categories successfully"
         };
     }, query);
    #endregion
}
