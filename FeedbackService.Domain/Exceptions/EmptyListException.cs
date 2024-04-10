namespace FeedbackService.Domain.Exceptions;

public class EmptyListException(string message) : Exception(message)
{
    public int StatusCode { get; } = 404;
}