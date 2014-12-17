using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository OrderRepository)
        {
            this._orderRepository = OrderRepository;
        }

        public int Count
        {
            get { return _orderRepository.Count; }
        }

        public IEnumerable<PhieuMua> All
        {
            get { return _orderRepository.All; }
        }        

        public int Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public int DeleteByCustomer(int CustomerId)
        {
            return _orderRepository.DeleteByCustomer(CustomerId);
        }
                
        public int Save(PhieuMua Order)
        {
            return _orderRepository.Save(Order);
        }

        public int LittleSave(PhieuMua Order)
        {
            return _orderRepository.LittleSave(Order);
        }

        public PhieuMua Find(int id)
        {
            return _orderRepository.Find(id);
        }

        public IEnumerable<SIM> FindCustomerOrderSIMs(int CustomerId)
        {
            return _orderRepository.FindCustomerOrderSIMs(CustomerId);
        }

        public PhieuMua FindOrderOfCustomerBySIM(int CustomerId, int SimId)
        {
            return _orderRepository.FindOrderOfCustomerBySIM(CustomerId, SimId);
        }

        public IEnumerable<PhieuMua> FindOrdersByCustomer(int CustomerId)
        {
            return _orderRepository.FindOrdersByCustomer(CustomerId);
        }

        public IEnumerable<SIM> FindSIMOrderInSevenDays()
        {
            return _orderRepository.FindSIMOrderInSevenDays();
        }

        public bool IsCustomerOutOfOrderTimes(int CustomerId)
        {
            return _orderRepository.IsCustomerOutOfOrderTimes(CustomerId);
        }

        public bool IsOrdered(int SimId)
        {
            return _orderRepository.IsOrdered(SimId);
        }

        public bool IsTheSame(int CustomerId, int SimId)
        {
            return _orderRepository.IsTheSame(CustomerId, SimId);
        }       
    }
}
