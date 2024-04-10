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
        _context.Feedbacks.Add(newFeedback);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Feedback feedback)
    {
        _context.Feedbacks.Remove(feedback);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<FeedbackDto> GetFeedbackById(int id)
    {
        var result = await(from feedback in _context.Feedbacks
                           join category in _context.Categories
                           on feedback.CategoryId equals category.Id
                           where feedback.Id == id
                           select new FeedbackDto
                           {
                               Id = feedback.Id,
                               CustomerName = feedback.CustomerName,
                               CategoryName = category.Name,
                               CategoryId = category.Id,
                               Description = feedback.Description,
                               SubmissionDate = feedback.SubmissionDate
                           }).FirstOrDefaultAsync();

        return result ?? throw new NotFoundException("Feedback was not found");
    }

    public async Task<List<FeedbackDto>> GetLastMonthAsync()
    {
        var startDate = DateTime.UtcNow.AddMonths(-1);
        var endDate = DateTime.UtcNow;

        var result = await (from feedback in _context.Feedbacks
                            join category in _context.Categories
                            on feedback.CategoryId equals category.Id
                            where feedback.SubmissionDate >= startDate && feedback.SubmissionDate <= endDate
                            select new FeedbackDto
                            {
                                Id = feedback.Id,
                                CustomerName = feedback.CustomerName,
                                CategoryName = category.Name,
                                CategoryId = category.Id,
                                Description = feedback.Description,
                                SubmissionDate = feedback.SubmissionDate
                            }).ToListAsync();

        if (result.Count == 0)
            throw new EmptyListException("The category table are empty");

        return result;
    }

    public async Task<bool> UpdateAsync(Feedback existingFeedback)
    {
        _context.Feedbacks.Update(existingFeedback);
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion
}
