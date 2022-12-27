using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;
        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public UserModel Register(UserModel userModel)
        {
            try
            {
                
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                //sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                sqlConnection.Open();

                string query = $"insert into Users values('{userModel.FullName}','{userModel.EmailId}','{EncryptPassword(userModel.Password)}','{userModel.PhoneNumber}')";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();

                return userModel;
               
                
            }
            catch(Exception)
            { 
                throw;            
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string Login(string emailId, string password)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"select * from Users where EmailId='{emailId}' and Password='{EncryptPassword(password)}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var userId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                if (userId != 0)
                {
                    var token = GenerateJSONWebToken(emailId,userId);
                    return token;
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

        public string ForgetPassword(string emailId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"select * from Users where EmailId='{emailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var userId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                if (userId != 0)
                {
                    var token = GenerateJSONWebToken(emailId, userId);
                    MSMQModel msmq = new MSMQModel();
                    msmq.sendData2Queue(token);
                    return token;
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

        public bool ResetPassword(string emailId, string password, string confirmPassword)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"select * from Users where EmailId='{emailId}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var userId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                if (userId != 0)
                {
                    query = $"update Users set Password = '{EncryptPassword(password)}' where EmailId = '{emailId}'";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.ExecuteNonQuery();

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

        public string GenerateJSONWebToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Role, "User"),
                   new Claim(ClaimTypes.Email, email),
                   new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encData = Convert.ToBase64String(encData_byte);
                return encData;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string DecryptPassword(string encData)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            System.Text.Decoder decoder = encoding.GetDecoder();
            byte[] decode_byte = Convert.FromBase64String(encData);
            int charCount = decoder.GetCharCount(decode_byte, 0, decode_byte.Length);
            char[] decoded_Char = new char[charCount];
            decoder.GetChars(decode_byte, 0, decode_byte.Length, decoded_Char, 0);
            string result = new string(decoded_Char);
            return result;
        }
    }
}
