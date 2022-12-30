using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class FeedbackRL : IFeedbackRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FeedbackModel AddFeddback(int userId, int bookId, FeedbackModel feedbackModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"insert into FeedbackTable values('{feedbackModel.Comment}',{feedbackModel.Rating}, {bookId}, {userId})";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    return feedbackModel;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<FeedbackModel> GetFeedback(int bookId)
        {
            try{
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                List<FeedbackModel> feedback = new List<FeedbackModel>();

                string query = $"select * from FeedbackTable where BookId = {bookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        FeedbackModel feedbackModel = new FeedbackModel()
                        {
                            FeedbackId = dataReader.GetInt32(0),
                            Comment = dataReader.GetString(1),
                            Rating = dataReader.GetInt32(2),
                            BookId = dataReader.GetInt32(3),
                            UserId = dataReader.GetInt32(4)
                        };
                        feedback.Add(feedbackModel);
                    }
                    return feedback;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"delete from FeedbackTable where FeedbackId = {feedbackId} and UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }

}
