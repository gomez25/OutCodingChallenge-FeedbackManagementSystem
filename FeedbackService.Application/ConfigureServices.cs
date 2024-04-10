using FeedbackService.Application.Commands.AddFeedback;
using FeedbackService.Application.Commands.DeleteFeedback;
using FeedbackService.Application.Commands.UpdateFeedbackCommand;
using FeedbackService.Application.Queries.GetCategories;
using FeedbackService.Application.Queries.GetFeedbackById;
using FeedbackService.Application.Queries.GetLastMonthFeedback;
using FeedbackService.Domain.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackService.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<AddFeedbackCommand, Response<bool>>, AddFeedbackCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateFeedbackCommand, Response<bool>>, UpdateFeedbackCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteFeedbackCommand, Response<bool>>, DeleteFeedbackCommandHandler>();
            services.AddTransient<IRequestHandler<GetCategoriesQuery, Response<GetCategoriesQueryResult>>, GetCategoriesQueryHandler>();
            services.AddTransient<IRequestHandler<GetFeedbackByIdQuery, Response<GetFeedbackByIdQueryResult>>, GetFeedbackByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetLastMonthFeedbackQuery, Response<GetLastMonthFeedbackQueryResult>>, GetLastMonthFeedbackQueryHandler>();

            services.AddTransient<AddFeedbackCommandValidator>();
            services.AddTransient<UpdateFeedbackCommandValidator>();
            services.AddTransient<DeleteFeedbackCommandValidator>();

            return services;
        }
    }
}
