using FeedbackManagementSystem.Models;
using FeedbackManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {
        private const string Baseurl = "http://localhost:5070/";
        public async Task<IActionResult> Index()
        {
            var response = await ServiceHelper.GetAsync<JObject>("api/Feedback");
            var feedbackList = response["getLastMonthFeedbackList"].ToObject<IEnumerable<FeedbackViewModel>>();

            return View(feedbackList);
        }
        public async Task<IActionResult> Create()
        {
            var response = await ServiceHelper.GetAsync<JObject>("api/Category");
            var categories = response["categories"].ToObject<IEnumerable<CategoryViewModel>>();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ServiceHelper.PostAsync("api/Feedback", model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var feedbackResponse = await ServiceHelper.GetAsync<FeedbackViewModel>($"api/Feedback/{id}");

            if (feedbackResponse != null)
            {
                var categoriesResponse = await ServiceHelper.GetAsync<JObject>("api/Category");
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
                await ServiceHelper.PutAsync("api/Feedback", model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await ServiceHelper.DeleteAsync($"api/Feedback/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}