using Microsoft.AspNetCore.Mvc;

namespace FeedbackManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
