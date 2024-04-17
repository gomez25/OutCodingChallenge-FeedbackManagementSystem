using FeedbackService.Domain.Constants;
using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;

namespace FeedbackService.Infrastructure.Persistence.Repositories;

internal class CategoryRepository(FeedbackContext context) : ICategoryRepository
{
    #region Variables
    private readonly FeedbackContext _context = context;
    #endregion

    #region Methods
    public async Task<IEnumerable<Category>> GetAll()
    {
        List<Category> categories = [];

        var result = await _context.ExecuteQueryAsync(StoredProcedureNames.GET_CATEGORIES);
        while (await result.ReadAsync())
        {
            Category category = new()
            {
                Id = Convert.ToInt32(result["Id"]),
                Name = result["Name"].ToString()
            };

            categories.Add(category);
        }
        if (categories.Count == 0)
            throw new EmptyListException("The category table are empty");

        return categories;
    }
    #endregion
}