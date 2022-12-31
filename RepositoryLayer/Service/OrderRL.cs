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
    public class OrderRL : IOrderRL
    {
        private  SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public OrderRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);

                SqlCommand sqlCommand = new SqlCommand("AddOrder", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@BookQuantity", order.Quantity);
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@BookId", order.BookId);
                sqlCommand.Parameters.AddWithValue("@AddressId", order.AddressId);
                sqlCommand.Parameters.AddWithValue("@BookCount", order.BookCount);

                this.sqlConnection.Open();
                int i = Convert.ToInt32(sqlCommand.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 3)
                {
                    return null;
                }

                if (i == 2)
                {
                    return null;
                }
                else
                {
                    return order;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand sqlCommand = new SqlCommand("GetAllOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    List<OrderModel> order = new List<OrderModel>();
                    while (reader.Read())
                    {
                        OrderModel orderModel = new OrderModel();
                        orderModel.OrderId = Convert.ToInt32(reader["OrderId"]);
                        orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                        orderModel.Quantity = Convert.ToInt32(reader["BookQuantity"]);
                        orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                        orderModel.BookId = Convert.ToInt32(reader["bookId"]);
                        orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                        order.Add(orderModel);
                    }
                    this.sqlConnection.Close();
                    return order;
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
                this.sqlConnection.Close();
            }
        }

        public bool DeleteOrder(int orderId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand sqlCommand = new SqlCommand("DeleteOrder", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@OrderId", orderId);
                this.sqlConnection.Open();
                int i = sqlCommand.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

    }

}
