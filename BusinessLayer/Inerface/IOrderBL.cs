using System;       
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessLayer.Inerface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel order, int userId);
        public List<OrderModel> GetAllOrder(int userId);
        public bool DeleteOrder(int orderId, int userId);

    }
}
