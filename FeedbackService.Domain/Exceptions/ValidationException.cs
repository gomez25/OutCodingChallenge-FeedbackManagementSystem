namespace FeedbackService.Domain.Exceptions;

public class ValidationException(string message) : Exception(message)
{
    public int StatusCode { get; } = 400;
}