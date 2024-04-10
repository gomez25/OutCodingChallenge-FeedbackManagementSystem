using FeedbackService.Domain.DTOs;
using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Infrastructure.Persistence.Repositories;

internal class FeedbackRepository(FeedbackContext context) : IFeedbackRepository
{
    #region Variables
    private readonly FeedbackContext _context = context;
    #endregion

    #region Methods
    public async Task<bool> AddAsync(Feedback newFeedback)
    {
        _context.Add(newFeedback);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<FeedbackDto>> GetLastMonthAsync()
    {
        var startDate = DateTime.Now.AddMonths(-1);
        var endDate = DateTime.Now;

        var result = await (from feedback in _context.Feedbacks
                            join category in _context.Categories
                            on feedback.CategoryId equals category.Id
                            where feedback.SubmissionDate >= startDate && feedback.SubmissionDate <= endDate
                            select new FeedbackDto
                            {
                                CustomerName = feedback.CustomerName,
                                CategoryName = category.Name,
                                Description = feedback.Description,
                                SubmissionDate = feedback.SubmissionDate
                            }).ToListAsync();

        if (result.Count == 0)
            throw new EmptyListException("The category table are empty");

        return result;
    }

    public async Task<bool> UpdateAsync(Feedback existingFeedback)
    {
        _context.Update(existingFeedback);
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion
}
