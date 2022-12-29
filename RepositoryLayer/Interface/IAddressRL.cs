using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public bool AddAddress(int userId, AddressModel addressModel);
        public bool UpdateAddress(int userId, AddressModel addressModel);
        public List<AddressModel> GetAllAddress(int userId);
        public bool DeleteAddress(int userId, int addressId);
    }
}
