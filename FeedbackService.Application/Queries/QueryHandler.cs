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
                var result = await action.Invoke(query);

                if (result != null)
                    return response;
            }
            catch (ArgumentNullException ex)
            {
                response.Message = ex.Message;
            }
            catch (ValidationException ex)
            {
                response.Message = ex.Message;
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