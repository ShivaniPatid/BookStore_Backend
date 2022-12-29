using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(int userId, AddressModel addressModel)
        {
            try
            {
                return addressRL.AddAddress(userId, addressModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateAddress(int userId, AddressModel addressModel)
        {
            try
            {
                return addressRL.UpdateAddress(userId, addressModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return addressRL.GetAllAddress(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteAddress(int userId, int addressId)
        {
            try
            {
                return addressRL.DeleteAddress(userId, addressId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
