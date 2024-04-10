using System;

namespace FeedbackService.Domain.DTOs
{
    public class FeedbackDto
    {
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}