using FeedbackService.Application.Commands.AddFeedback;
using FeedbackService.Application.Commands.UpdateFeedbackCommand;
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
            services.AddTransient<IRequestHandler<GetLastMonthFeedbackQuery, Response<GetLastMonthFeedbackQueryResult>>, GetLastMonthFeedbackQueryHandler>();

            services.AddTransient<AddFeedbackCommandValidator>();
            services.AddTransient<UpdateFeedbackCommandValidator>();


            return services;
        }
    }
}
