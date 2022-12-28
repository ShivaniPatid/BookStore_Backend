using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public bool AddCart(int userId, int bookId, int quantity);
        public bool UpdateCart(int quantity, int userId, int cartId);
        public bool DeleteCart(int userId, int cartId);
        public List<CartModel> GetCartDetails(int userId);
    }
}
