namespace FeedbackService.Domain.Exceptions;

public class ParametersNullException(string message) : Exception(message)
{
    public int StatusCode { get; } = 400;
}