using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Infrastructure.Persistence.Repositories;

internal class CategoryRepository(FeedbackContext context) : ICategoryRepository
{
    #region Variables
    private readonly FeedbackContext _context = context;
    #endregion

    #region Methods
    public async Task<IEnumerable<Category>> GetAll()
    {
        var result = await _context.Categories.ToListAsync();

        if (result.Count == 0)
            throw new EmptyListException("The category table are empty");

        return result;
    }
    #endregion
}