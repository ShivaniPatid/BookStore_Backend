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
    public class AddressRL : IAddressRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public AddressRL (IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddAddress(int userId, AddressModel addressModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"insert into AddressTable values('{addressModel.FullAddress}','{addressModel.City}'," +
                    $"'{addressModel.State}',{addressModel.TypeId},{userId})";
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

        public bool UpdateAddress(int userId, AddressModel addressModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"update AddressTable set FullAddress = '{addressModel.FullAddress}', City = '{addressModel.City}'," +
                    $" State = '{addressModel.State}', TypeId = {addressModel.TypeId} where AddressId = {addressModel.AddressId} and " +
                    $" UserId = {userId} ";
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

        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                SqlCommand sqlCommand = new SqlCommand("GetAllAddress", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    List<AddressModel> addressModel = new List<AddressModel>();
                    while (dataReader.Read())
                    {
                        addressModel.Add(new AddressModel
                        {
                            AddressId = Convert.ToInt32(dataReader["AddressId"]),
                            FullAddress = dataReader["FullAddress"].ToString(),
                            City = dataReader["City"].ToString(),
                            State = dataReader["State"].ToString(),
                            TypeId = Convert.ToInt32(dataReader["TypeId"]),
                            UserId = Convert.ToInt32(dataReader["UserId"])
                        });
                    }
                    sqlConnection.Close();
                    return addressModel;
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

        public bool DeleteAddress(int userId, int addressId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();
                string query = $"delete from AddressTable where UserId = {userId} and AddressId = {addressId}";
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
