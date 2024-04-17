namespace FeedbackService.Domain.Constants;

public static class StoredProcedureNames
{
    public const string GET_CATEGORIES = "sp_GetCategories";
    public const string GET_FEEDBACKSBYDATESRANGE = "sp_GetFeedbacksByDatesRange";
    public const string GET_FEEDBACKBYID = "sp_GetFeedbackById";
    public const string ADD_FEEDBACK = "sp_AddFeedback";
    public const string UPDATE_FEEDBACK = "sp_UpdateFeedback";
    public const string DELETE_FEEDBACK = "sp_DeleteFeedback";
}
