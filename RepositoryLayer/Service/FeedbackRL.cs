using System;
using System.Collections.Generic;
using System.Data;
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
                SqlCommand sqlCommand = new SqlCommand("GetAllFeedback", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    List<FeedbackModel> feedbackModel = new List<FeedbackModel>();

                    while (dataReader.Read())
                    {
                        FeedbackModel feedback = new FeedbackModel();

                        UserModel user = new UserModel
                        {
                            FullName = dataReader["FullName"].ToString(),
                        };
                        feedback.FeedbackId = Convert.ToInt32(dataReader["FeedbackId"]);
                        feedback.Comment = dataReader["Comment"].ToString();
                        feedback.Rating = Convert.ToInt32(dataReader["Rating"]);
                        feedback.BookId = Convert.ToInt32(dataReader["BookId"]);
                        feedback.UserModel = user;
                        feedbackModel.Add(feedback);
                    }
                    sqlConnection.Close();
                    return feedbackModel;
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
