using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedbackModel AddFeddback(int userId, int bookId, FeedbackModel feedbackModel)
        {
            try
            {
                return feedbackRL.AddFeddback(userId, bookId, feedbackModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<FeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                return feedbackRL.GetFeedback(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                return feedbackRL.DeleteFeedback(feedbackId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
