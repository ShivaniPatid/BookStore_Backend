using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel Register (UserModel userModel);
        public string Login(string emailId, string password);
        public string ForgetPassword(string emailId);
        public bool ResetPassword(string emailId, string password, string confirmPassword);


    }
}
