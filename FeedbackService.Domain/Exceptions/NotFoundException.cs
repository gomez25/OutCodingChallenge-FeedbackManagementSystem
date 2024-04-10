using System;

namespace FeedbackService.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
        public int StatusCode { get; } = 404;
    }

}