﻿namespace FeedbackService.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public int StatusCode { get; } = 404;
}