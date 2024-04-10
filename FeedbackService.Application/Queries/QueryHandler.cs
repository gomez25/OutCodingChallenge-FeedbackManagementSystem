using FeedbackService.Domain.Exceptions;
using FeedbackService.Domain.Shared;
using MediatR;

namespace FeedbackService.Application.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Response<TResult>>
        where TQuery : IRequest<Response<TResult>>
    {
        public async Task<Response<TResult>> ServiceHandlerAsync(Func<TQuery, Task<Response<TResult>>> action, TQuery query)
        {
            var response = new Response<TResult>();
            try
            {
                response = await action.Invoke(query);

                if (response != null)
                    return response;
            }
            catch (ValidationException ex)
            {
                response.Message = ex.Message;
                response.StatusCode = ex.StatusCode;
            }
            catch (EmptyListException ex)
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
        public abstract Task<Response<TResult>> HandleAsync(TQuery query);

        public Task<Response<TResult>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return HandleAsync(request);
        }
    }
}