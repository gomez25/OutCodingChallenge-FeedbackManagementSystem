using FeedbackService.Domain.Entities.Common;

namespace FeedbackService.Domain.Entities
{
    public class Category : Entity
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        #endregion

        #region Foreign Keys
        public ICollection<Feedback> Feedbacks { get; set; }
        #endregion
    }
}
