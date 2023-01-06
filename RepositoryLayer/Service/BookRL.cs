using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {

                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"insert into Books values('{bookModel.BookName}','{bookModel.AuthorName}','{bookModel.BookDescription}','{bookModel.BookImage}','{bookModel.Rating}','{bookModel.Totalview}','{bookModel.OriginalPrice}','{bookModel.DiscountedPrice}',{bookModel.Quantity} )";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                if (result > 0)
                {
                    return bookModel;
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

        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"update Books set BookName = '{bookModel.BookName}',AuthoreName = '{bookModel.AuthorName}'," +
                    $" BookDescription = '{bookModel.BookDescription}', BookImage = '{bookModel.BookImage}', " +
                    $"Rating = '{bookModel.Rating}', TotalView = '{bookModel.Totalview}',OriginalPrice = '{bookModel.OriginalPrice}'," +
                    $" DiscountPrice = '{bookModel.DiscountedPrice}', Quantity = '{bookModel.Quantity}' where BookId = '{bookModel.BookId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                if (result > 0)
                {
                    return bookModel;
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

        public bool DeleteBook(int bookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"delete from Books where BookId = {bookId}";
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

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query =" select * from Books";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        BookModel bookModel = new BookModel()
                        {
                            BookId = dataReader.GetInt32(0),
                            BookName = dataReader.GetString(1),
                            AuthorName = dataReader.GetString(2),
                            BookDescription = dataReader.GetString(3),
                            BookImage = dataReader.GetString(4),
                            Rating = (float)dataReader.GetDouble(5),
                            Totalview = dataReader.GetInt32(6),
                            OriginalPrice = (decimal)dataReader.GetDouble(7),
                            DiscountedPrice = (decimal)dataReader.GetDouble(8),
                            Quantity = dataReader.GetInt32(9),
                        };
                         books.Add(bookModel);
                        
                    }
                    return books;
                }
                else return null;
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

        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $" select * from Books where BookId = {bookId}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    BookModel bookModel = new BookModel();

                    while (dataReader.Read())
                    {

                        bookModel.BookId = dataReader.GetInt32(0);
                        bookModel.BookName = dataReader.GetString(1);
                        bookModel.AuthorName = dataReader.GetString(2);
                        bookModel.BookDescription = dataReader.GetString(3);
                        bookModel.BookImage = dataReader.GetString(4);
                        bookModel.Rating = (float)dataReader.GetDouble(5);
                        bookModel.Totalview = dataReader.GetInt32(6);
                        bookModel.OriginalPrice = (decimal)dataReader.GetDouble(7);
                        bookModel.DiscountedPrice = (decimal)dataReader.GetDouble(8);
                        bookModel.Quantity = dataReader.GetInt32(9);
                    }
                    return bookModel;

                }
                else
                {
                    return null;
                }
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
