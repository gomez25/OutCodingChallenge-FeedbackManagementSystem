using FeedbackManagementSystem.Models;
using FeedbackManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly string BaseUrl;
        public FeedbackController(IConfiguration configuration)
        {

            BaseUrl = configuration["ApiBaseUrl"]; ;

        }
        public async Task<IActionResult> Index()
        {
            var response = await ServiceHelper.GetAsync<JObject>(BaseUrl,"api/Feedback");
            if (response == null)
            {
                return View("Index", new List<CategoryFeedbackViewModel>());
            }
            var feedbackList = response["getLastMonthFeedbackList"].ToObject<IEnumerable<CategoryFeedbackViewModel>>();

            return View(feedbackList);
        }

        public async Task<IActionResult> Create()
        {
            var response = await ServiceHelper.GetAsync<JObject>(BaseUrl, "api/Category");
            var categories = response["categories"].ToObject<IEnumerable<CategoryViewModel>>();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ServiceHelper.PostAsync(BaseUrl, "api/Feedback", model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var feedbackResponse = await ServiceHelper.GetAsync<FeedbackViewModel>(BaseUrl, $"api/Feedback/{id}");

            if (feedbackResponse != null)
            {
                var categoriesResponse = await ServiceHelper.GetAsync<JObject>(BaseUrl, "api/Category");
                var categories = categoriesResponse["categories"].ToObject<IEnumerable<CategoryViewModel>>();

                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                return View(feedbackResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ServiceHelper.PutAsync(BaseUrl, "api/Feedback", model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await ServiceHelper.DeleteAsync(BaseUrl, $"api/Feedback/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}