using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddCart(int userId , int bookId, int quantity)
        {
            try
            {
                return cartRL.AddCart(userId, bookId, quantity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateCart(int quantity, int userId, int cartId)
        {
            try
            {
                return cartRL.UpdateCart(quantity, userId, cartId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCart(int userId, int cartId)
        {
            try
            {
                return cartRL.DeleteCart(userId, cartId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                return cartRL.GetCartDetails(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
