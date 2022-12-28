using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {
        private static SqlConnection sqlConnection;
        private readonly IConfiguration configuration;

        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AdminLogin(string emailId, string password)
        {
            try
            {
                sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]);
                sqlConnection.Open();

                string query = $"select * from AdminTable where EmailId='{emailId}' and Password='{password}'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                var adminId = Convert.ToInt64(sqlCommand.ExecuteScalar());

                if (adminId != 0)
                {
                    var token = GenerateJSONWebToken(emailId, adminId);
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


        public string GenerateJSONWebToken(string email, long adminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.Role, "Admin"),
                   new Claim(ClaimTypes.Email, email),
                   new Claim("AdminId", adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
