using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Inerface;
using CommonLayer.Models;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                return orderRL.AddOrder(order, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                return orderRL.GetAllOrder(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteOrder(int orderId, int userId)
        {
            try
            {
                return orderRL.DeleteOrder(orderId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
