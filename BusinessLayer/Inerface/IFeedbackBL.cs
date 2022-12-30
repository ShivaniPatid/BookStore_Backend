using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessLayer.Inerface
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeddback(int userId, int bookId, FeedbackModel feedbackModel);
        public List<FeedbackModel> GetFeedback(int bookId);
        public bool DeleteFeedback(int feedbackId, int userId);

    }
}
