using FeedbackService.Domain.Entities.Common;

namespace FeedbackService.Domain.Entities
{
    public class Feedback : Entity
    {
        #region Properties
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int CategoryId { get; set; }
        #endregion

        #region Foreign Keys
        public Category Category { get; set; }
        #endregion
    }
}
