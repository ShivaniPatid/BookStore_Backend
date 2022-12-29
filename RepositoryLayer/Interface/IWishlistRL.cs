using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public bool AddToWishlist(int userId, int bookId);
        public List<WishlistModel> ViewWishlist(int userId);
        public bool DeleteFromWishlist(int userId, int wishlistId);
    }
}
