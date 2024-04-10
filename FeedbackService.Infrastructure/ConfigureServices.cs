using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;
using FeedbackService.Infrastructure.Persistence.Repositories;
using FeedbackService.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FeedbackService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        //Add Context
        services.AddDbContext<FeedbackContext>(opt => 
        opt.UseSqlServer(configuration.GetConnectionString("Db"), a => a.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));


        return services;
    }
}