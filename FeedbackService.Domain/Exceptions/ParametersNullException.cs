using System;

namespace FeedbackService.Domain.Exceptions
{
    public class ParametersNullException : Exception
    {
        public ParametersNullException(string message) : base(message)
        {

        }
        public int StatusCode { get; } = 400;
    }

}