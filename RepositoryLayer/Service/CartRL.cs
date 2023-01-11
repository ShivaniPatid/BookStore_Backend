using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class CartRL : ICartRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddCart(int userId, int bookId, int quantity)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"insert into Cart values({quantity},{userId},{bookId})";
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

        public bool UpdateCart(int quantity, int userId, int cartId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"update Cart set Quantity = {quantity} where CartId = {cartId} and UserId = {userId}";
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

        public bool DeleteCart(int userId, int cartId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"delete from Cart where CartId = {cartId} and UserId = {userId}";
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

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand sqlCommand = new SqlCommand("GetCartByUserId", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@UserId", userId);

                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                
                if(dataReader.HasRows)
                {
                    List<CartModel> cartModel = new List<CartModel>();
                    while (dataReader.Read())
                    {
                        BookModel book = new BookModel();
                        CartModel cart = new CartModel();
                        book.BookName = dataReader["bookName"].ToString();
                        book.AuthorName = dataReader["AuthoreName"].ToString();
                        book.OriginalPrice = Convert.ToDecimal(dataReader["originalPrice"]);
                        book.DiscountedPrice = Convert.ToDecimal(dataReader["discountPrice"]);
                        book.BookImage = dataReader["bookImage"].ToString();
                        cart.UserId = Convert.ToInt32(dataReader["UserId"]);
                        cart.BookId = Convert.ToInt32(dataReader["BookId"]);
                        cart.CartId = Convert.ToInt32(dataReader["CartId"]);
                        cart.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        cart.Bookmodel = book;
                        cartModel.Add(cart);
                    }
                    sqlConnection.Close();
                    return cartModel;
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

    }
}
