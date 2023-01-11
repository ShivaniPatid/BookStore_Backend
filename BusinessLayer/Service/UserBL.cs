using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserModel Register(UserModel userModel)
        {
            try
            {
                return userRL.Register(userModel);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public string Login(string emailId, string password)
        {
            try
            {
                return userRL.Login(emailId, password);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string emailId)
        {
            try
            {
                return userRL.ForgetPassword(emailId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string emailId, string password, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(emailId, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserModel GetUser(string emailId)
        {
            try
            {
                return userRL.GetUser(emailId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
