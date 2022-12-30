using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeddback(int userId, int bookId, FeedbackModel feedbackModel);
        public List<FeedbackModel> GetFeedback(int bookId);
        public bool DeleteFeedback(int feedbackId, int userId);

    }
}
