using FeedbackService.Application.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        #region Variables
        private readonly IMediator _mediator = mediator;
        #endregion

        #region Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetCategoriesQuery());
            if (response.Success)
                return Ok(response.Data);
            else
                return StatusCode(response.StatusCode, response.Message);
        }
        #endregion
    }
}