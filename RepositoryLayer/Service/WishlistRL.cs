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
                SqlCommand sqlCommand = new SqlCommand("GetAllRecordFromWishlist", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    List<WishlistModel> wishModel = new List<WishlistModel>();

                    while (dataReader.Read())
                    {
                        BookModel book = new BookModel();
                        WishlistModel wish = new WishlistModel();
                        book.BookName = dataReader["BookName"].ToString();
                        book.AuthorName = dataReader["AuthoreName"].ToString();
                        book.DiscountedPrice = Convert.ToDecimal(dataReader["DiscountPrice"]);
                        book.OriginalPrice = Convert.ToDecimal(dataReader["OriginalPrice"]);
                        book.BookImage = dataReader["BookImage"].ToString();
                        wish.WishlistId = Convert.ToInt32(dataReader["WishlistId"]);
                        wish.UserId = Convert.ToInt32(dataReader["UserId"]);
                        wish.BookId = Convert.ToInt32(dataReader["BookId"]);
                        wish.Bookmodel = book;
                        wishModel.Add(wish);

                    }
                    sqlConnection.Close();
                    return wishModel;
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

