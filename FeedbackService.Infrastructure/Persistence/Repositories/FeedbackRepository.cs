using FeedbackService.Domain.Constants;
using FeedbackService.Domain.DTOs;
using FeedbackService.Domain.Entities;
using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Repositories;
using FeedbackService.Infrastructure.Persistence.Contexts;
using System.Data;
using System.Data.SqlClient;

namespace FeedbackService.Infrastructure.Persistence.Repositories;

internal class FeedbackRepository(FeedbackContext context) : IFeedbackRepository
{
    #region Variables
    private readonly FeedbackContext _context = context;
    #endregion

    #region Methods
    public async Task<bool> AddAsync(Feedback newFeedback)
    {
        SqlParameter[] parameters =
        [
            new SqlParameter("@CustomerName", SqlDbType.NVarChar) { Value = newFeedback.CustomerName },
            new SqlParameter("@Description", SqlDbType.NVarChar) { Value = newFeedback.Description },
            new SqlParameter("@CategoryId", SqlDbType.Int) { Value = newFeedback.CategoryId },
        ];
        return await _context.ExecuteNonQueryAsync(StoredProcedureNames.ADD_FEEDBACK, parameters: parameters);
    }

    public async Task<bool> DeleteAsync(int feedbackId)
    {
        SqlParameter[] parameters = [new SqlParameter("@FeedbackId", SqlDbType.Int) { Value = feedbackId }];
        return await _context.ExecuteNonQueryAsync(StoredProcedureNames.DELETE_FEEDBACK, parameters: parameters);
    }

    public async Task<FeedbackDto> GetFeedbackById(int id)
    {
        FeedbackDto response = default;
        SqlParameter[] parameters =
        [
            new SqlParameter("@Id", SqlDbType.Int) { Value = id },
        ];
        var result = await _context.ExecuteQueryAsync(StoredProcedureNames.GET_FEEDBACKBYID, parameters: parameters);

        while (await result.ReadAsync())
        {
            response = new()
            {
                Id = Convert.ToInt32(result["FeedbackId"]),
                CustomerName = result["CustomerName"].ToString(),
                Description = result["Description"].ToString(),
                SubmissionDate = Convert.ToDateTime(result["SubmissionDate"]),
                CategoryId = Convert.ToInt32(result["CategoryId"]),
                CategoryName = result["CategoryName"].ToString()
            };
        }

        return response ?? throw new NotFoundException("Feedback was not found");

    }

    public async Task<IEnumerable<CategoryFeedbackDto>> GetLastMonthAsync()
    {
        var startDate = DateTime.UtcNow.AddMonths(-1);
        var endDate = DateTime.UtcNow;


        List<CategoryFeedbackFromSpDto> feedbacks = [];
        List<CategoryFeedbackDto> feedbacksGroupingByCategory = [];

        SqlParameter[] parameters =
        [
            new SqlParameter("@StartDate", SqlDbType.DateTime) { Value = startDate },
            new SqlParameter("@EndDate", SqlDbType.DateTime) { Value = endDate },
        ];
        var result = await _context.ExecuteQueryAsync(StoredProcedureNames.GET_FEEDBACKSBYDATESRANGE, parameters: parameters);

        while (await result.ReadAsync())
        {
            CategoryFeedbackFromSpDto feedback = new()
            {
                FeedbackId = Convert.ToInt32(result["FeedbackId"]),
                CustomerName = result["CustomerName"].ToString(),
                Description = result["Description"].ToString(),
                SubmissionDate = Convert.ToDateTime(result["SubmissionDate"]),
                CategoryId = Convert.ToInt32(result["CategoryId"]),
                CategoryName = result["CategoryName"].ToString(),
            };

            feedbacks.Add(feedback);
        }
        if (feedbacks.Count == 0)
            throw new EmptyListException("The category table are empty");

        return ConvertToCategoryFeedbackDto(feedbacks);
    }

    public async Task<bool> UpdateAsync(Feedback existingFeedback)
    {
        SqlParameter[] parameters =
        [
            new SqlParameter("@FeedbackId", SqlDbType.Int) { Value = existingFeedback.Id },
            new SqlParameter("@CustomerName", SqlDbType.NVarChar) { Value = existingFeedback.CustomerName },
            new SqlParameter("@Description", SqlDbType.NVarChar) { Value = existingFeedback.Description },
            new SqlParameter("@CategoryId", SqlDbType.Int) { Value = existingFeedback.CategoryId }
        ];

        return await _context.ExecuteNonQueryAsync(StoredProcedureNames.UPDATE_FEEDBACK, parameters: parameters);
    }

    #endregion

    #region Private Methods
    private static List<CategoryFeedbackDto> ConvertToCategoryFeedbackDto(List<CategoryFeedbackFromSpDto> categoryFeedbackFromSpDtos)
    {
        var categoryFeedbackDtos = categoryFeedbackFromSpDtos
            .GroupBy(dto => new { dto.CategoryId, dto.CategoryName })
            .Select(group => new CategoryFeedbackDto
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                Feedbacks = group.Select(dto => new Feedback
                {
                    Id = dto.FeedbackId,
                    CustomerName = dto.CustomerName,
                    Description = dto.Description,
                    SubmissionDate = dto.SubmissionDate
                })
            })
            .ToList();

        return categoryFeedbackDtos;
    }
    #endregion
}
