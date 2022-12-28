using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inerface
{
    public interface IAdminBL
    {
        public string AdminLogin(string emailId, string password);

    }
}
