using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessLayer.Inerface
{
    public interface IAddressBL
    {
        public bool AddAddress(int userId, AddressModel addressModel);
        public bool UpdateAddress(int userId, AddressModel addressModel);
        public List<AddressModel> GetAllAddress(int userId);
        public bool DeleteAddress(int userId, int addressId);

    }
}
