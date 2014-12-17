using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public interface IOrderRepository : IEntityRepository<PhieuMua>
    {
        int DeleteByCustomer(int CustomerId);
        IEnumerable<SIM> FindCustomerOrderSIMs(int CustomerId);
        PhieuMua FindOrderOfCustomerBySIM(int CustomerId, int SimId);
        IEnumerable<PhieuMua> FindOrdersByCustomer(int CustomerId);
        IEnumerable<SIM> FindSIMOrderInSevenDays();
        bool IsCustomerOutOfOrderTimes(int CustomerId);
        bool IsOrdered(int SimId);
        bool IsTheSame(int CustomerId, int SimId);
        int LittleSave(PhieuMua Order);
    }
}
