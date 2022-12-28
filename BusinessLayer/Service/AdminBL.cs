using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(string emailId, string password)
        {
            try
            {
                return adminRL.AdminLogin(emailId,password);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
