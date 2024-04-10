using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Queries.GetLastMonthFeedback
{
    public class GetLastMonthFeedbackQuery : IRequest<Response<GetLastMonthFeedbackQueryResult>>
    {
    }
}