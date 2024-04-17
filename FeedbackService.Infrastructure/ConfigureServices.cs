using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;
using FeedbackService.Infrastructure.Persistence.Repositories;
using FeedbackService.Infrastructure.Persistence.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        services.AddSingleton(new FeedbackContext(configuration.GetConnectionString("Db")));

        return services;
    }
}