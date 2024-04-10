using FluentValidation.Results;
using System.Text.Json;

namespace FeedbackService.Application.Extensions;

public static class StringExtensions
{
    public static string ErrorsToString(this IEnumerable<ValidationFailure> errors) => JsonSerializer.Serialize(errors);
}