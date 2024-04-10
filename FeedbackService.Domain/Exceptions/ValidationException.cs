using System;

namespace FeedbackService.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
        public int StatusCode { get; } = 400;
    }

}