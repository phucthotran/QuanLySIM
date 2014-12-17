using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    class FakeOrderRepository : IOrderRepository
    {
        IList<PhieuMua> orders;

        public FakeOrderRepository()
        {
            orders = new List<PhieuMua>
            {
                new PhieuMua { MaPM = 1, MaKH = 2, NgayDatMua = DateTime.Parse("10/20/2013"), NgayGiao = DateTime.Parse("10/25/2013"), TongTien = 899, SimId = 1, SIM = new FakeSimRepository().Find(1), KhachHang = new FakeCustomerRepository().Find(2) },
                new PhieuMua { MaPM = 2, MaKH = 2, NgayDatMua = DateTime.Parse("10/20/2013"), NgayGiao = DateTime.Parse("10/25/2013"), TongTien = 459, SimId = 2, SIM = new FakeSimRepository().Find(2), KhachHang = new FakeCustomerRepository().Find(2) },
                new PhieuMua { MaPM = 3, MaKH = 3, NgayDatMua = DateTime.Parse("10/20/2013"), NgayGiao = DateTime.Parse("10/25/2013"), TongTien = 639, SimId = 6, SIM = new FakeSimRepository().Find(6), KhachHang = new FakeCustomerRepository().Find(3) },
                new PhieuMua { MaPM = 4, MaKH = 6, NgayDatMua = DateTime.Parse("10/20/2013"), NgayGiao = DateTime.Parse("10/25/2013"), TongTien = 378, SimId = 4, SIM = new FakeSimRepository().Find(4), KhachHang = new FakeCustomerRepository().Find(6) }
            };
        }

        public int DeleteByCustomer(int CustomerId)
        {
            PhieuMua order = orders.Where(x => x.MaKH == CustomerId).SingleOrDefault();
            orders.Remove(order);

            return 1;
        }

        public IEnumerable<Entities.SIM> FindCustomerOrderSIMs(int CustomerId)
        {
            return orders.Where(x => x.MaKH == CustomerId).Select<PhieuMua, SIM>(x => x.SIM);
        }

        public Entities.PhieuMua FindOrderOfCustomerBySIM(int CustomerId, int SimId)
        {
            return orders.Where(x => x.MaKH == CustomerId && x.SimId == SimId).SingleOrDefault();
        }

        public IEnumerable<Entities.PhieuMua> FindOrdersByCustomer(int CustomerId)
        {
            return orders.Where(x => x.MaKH == CustomerId);
        }

        public IEnumerable<Entities.SIM> FindSIMOrderInSevenDays()
        {
            return orders.Where(x => (DateTime.Now.Day - x.NgayDatMua.Day) <= 7).Select<PhieuMua, SIM>(x => x.SIM);
        }

        public bool IsCustomerOutOfOrderTimes(int CustomerId)
        {
            return orders.Where(x => x.MaKH == CustomerId && x.SIM.TinhTrang == SIM.NOT_PAID).Count() >= 2;
        }

        public bool IsOrdered(int SimId)
        {
            return orders.Where(x => x.SimId == SimId).SingleOrDefault() != null;
        }

        public bool IsTheSame(int CustomerId, int SimId)
        {
            return orders.Where(x => x.MaKH == CustomerId && x.SimId == SimId).SingleOrDefault() != null;
        }

        public int LittleSave(Entities.PhieuMua Order)
        {
            orders.Add(Order);

            return 1;
        }

        public int Count
        {
            get { return orders.Count; }
        }

        public IEnumerable<Entities.PhieuMua> All
        {
            get { return orders; }
        }

        public Entities.PhieuMua Find(int id)
        {
            return orders.Where(x => x.MaPM == id).SingleOrDefault();
        }

        public int Save(Entities.PhieuMua Entity)
        {
            if (Entity.MaPM != 0)
            {
                PhieuMua original = Find(Entity.MaPM);
                original.NgayDatMua = Entity.NgayDatMua;
                original.NgayGiao = Entity.NgayGiao;
                original.TongTien = Entity.TongTien;
                original.SimId = Entity.SimId;

                return 1;
            }

            int countBefore = orders.Count;

            orders.Add(Entity);

            return orders.Count - countBefore;
        }

        public int Delete(int id)
        {
            int countBefore = orders.Count;

            orders.Remove(Find(id));

            return countBefore - orders.Count;
        }
    }
}
