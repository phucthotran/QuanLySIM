using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface IOrderService
    {
        IEnumerable<PhieuMua> All { get; }
        int Count { get; }
        int Delete(int id);
        int DeleteByCustomer(int CustomerId);
        PhieuMua Find(int id);
        IEnumerable<SIM> FindCustomerOrderSIMs(int CustomerId);
        PhieuMua FindOrderOfCustomerBySIM(int CustomerId, int SimId);
        IEnumerable<PhieuMua> FindOrdersByCustomer(int CustomerId);
        IEnumerable<SIM> FindSIMOrderInSevenDays();
        bool IsCustomerOutOfOrderTimes(int CustomerId);
        bool IsOrdered(int SimId);
        bool IsTheSame(int CustomerId, int SimId);
        int LittleSave(PhieuMua Order);
        int Save(PhieuMua Order);
    }
}
