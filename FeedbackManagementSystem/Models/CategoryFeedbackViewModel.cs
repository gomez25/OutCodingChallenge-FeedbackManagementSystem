using System.Collections.Generic;

namespace FeedbackManagementSystem.Models
{
    public class CategoryFeedbackViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}
