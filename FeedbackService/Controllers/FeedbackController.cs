using FeedbackService.Application.Commands.AddFeedback;
using FeedbackService.Application.Commands.DeleteFeedback;
using FeedbackService.Application.Commands.UpdateFeedbackCommand;
using FeedbackService.Application.Queries.GetFeedbackById;
using FeedbackService.Application.Queries.GetLastMonthFeedback;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController(IMediator mediator) : ControllerBase
    {
        #region Variables
        private readonly IMediator _mediator = mediator;
        #endregion

        #region Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetLastMonthFeedbackQuery());
            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var response = await _mediator.Send(new GetFeedbackByIdQuery() { Id = id});
            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFeedbackCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFeedbackCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteFeedbackCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        #endregion
    }
}