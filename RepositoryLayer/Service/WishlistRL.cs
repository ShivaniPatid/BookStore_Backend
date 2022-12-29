using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class WishlistRL : IWishlistRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public WishlistRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddToWishlist(int userId, int bookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"insert into Wishlist values({userId},{bookId})";
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

        public List<WishlistModel> ViewWishlist(int userId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                List<WishlistModel> wishlist = new List<WishlistModel>();

                string query = $"select * from Wishlist where UserId = {userId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        WishlistModel wishlistModel = new WishlistModel()
                        {
                            WishlistId = dataReader.GetInt32(0),
                            UserId = dataReader.GetInt32(1),
                            BookId = dataReader.GetInt32(2)
                        };
                        wishlist.Add(wishlistModel);
                    }
                    return wishlist;
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

        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"delete from Wishlist where WishlistId = {wishlistId} and UserId = {userId}";
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

