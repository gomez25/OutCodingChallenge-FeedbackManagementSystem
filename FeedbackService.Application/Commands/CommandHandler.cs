using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Commands;

public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Response<TResult>>
    where TCommand : IRequest<Response<TResult>>
{
    public async Task<Response<TResult>> ServiceHandlerAsync(Func<TCommand, Task<Response<TResult>>> action, TCommand command)
    {
        var response = new Response<TResult>();
        try
        {
            AssertCommandValid(command);

            response = await action.Invoke(command);

            if (response.Success)
                return response;
        }
        catch (ParametersNullException ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (ValidationException ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.StatusCode = 500;
        }
        return response;
    }

    protected abstract Task<Response<TResult>> HandleAsync(TCommand command);
    protected virtual void AssertCommandValid(TCommand command)
    {
        if (command == null)
            throw new ParametersNullException("Cannot be null or empty");
    }

    public Task<Response<TResult>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return HandleAsync(request);
    }
}