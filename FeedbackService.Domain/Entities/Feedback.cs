using FeedbackService.Domain.Entities.Common;
using System;
using System.Collections.Generic;

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
        public ICollection<Category> Categories { get; set; }
        #endregion
    }
}
