using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        IList<KhachHang> customers;

        public FakeCustomerRepository()
        {
            customers = new List<KhachHang>
            {
                new KhachHang { MaKH = 1, TenTK = "user1234", MatKhau = "pass1234", Email = "email1@gmail.com", TenKH = "Khach Hang 1", DiaChi = "Dia Chi 1", CMND = "111111111", SDT = "011111111", SoLuongDaMua = 1, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) },
                new KhachHang { MaKH = 2, TenTK = "user2234", MatKhau = "pass2234", Email = "email2@gmail.com", TenKH = "Khach Hang 2", DiaChi = "Dia Chi 2", CMND = "211111111", SDT = "021111111", SoLuongDaMua = 2, MaNV = 2, NhanVien = new FakeStaffRepository().Find(2) },
                new KhachHang { MaKH = 3, TenTK = "user3234", MatKhau = "pass3234", Email = "email3@gmail.com", TenKH = "Khach Hang 3", DiaChi = "Dia Chi 3", CMND = "311111111", SDT = "031111111", SoLuongDaMua = 3, MaNV = 3, NhanVien = new FakeStaffRepository().Find(3) },
                new KhachHang { MaKH = 4, TenTK = "user4234", MatKhau = "pass4234", Email = "email4@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "411111111", SDT = "041111111", SoLuongDaMua = 4, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) },
                new KhachHang { MaKH = 5, TenTK = "user4234", MatKhau = "pass4234", Email = "email2@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "411111111", SDT = "051111111", SoLuongDaMua = 5, MaNV = 2, NhanVien = new FakeStaffRepository().Find(2) },
                new KhachHang { MaKH = 6, TenTK = "user6234", MatKhau = "pass4234", Email = "email6@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "611111111", SDT = "041111111", SoLuongDaMua = 6, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) }
            };
        }

        public int Available(string Username, string Password)
        {
            KhachHang c = customers.Where(x => x.TenTK == Username && x.MatKhau == Password).SingleOrDefault();
            return c == null ? 0 : c.MaKH;
        }

        public bool IsAccountExist(string Username)
        {
            return customers.Where(x => x.TenTK == Username).SingleOrDefault() != null;
        }

        public bool IsEmailExist(string Email)
        {
            return customers.Where(x => x.Email == Email).SingleOrDefault() != null;
        }

        public bool IsIdCardExist(string IdCardNumber)
        {
            return customers.Where(x => x.CMND == IdCardNumber).SingleOrDefault() != null;
        }

        public bool IsPhoneNumExist(string PhoneNum)
        {
            return customers.Where(x => x.SDT == PhoneNum).SingleOrDefault() != null;
        }

        public int Save(Entities.KhachHang Customer, bool PasswordNotHash)
        {
            if (Customer.MaKH != 0)
            {
                Customer.MatKhau = PasswordNotHash == true ? Customer.MatKhau : Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau);

                KhachHang original = Find(Customer.MaKH);
                original.TenTK = Customer.TenTK;
                original.MatKhau = Customer.MatKhau;
                original.Email = Customer.Email;
                original.TenKH = Customer.TenKH;
                original.SDT = Customer.SDT;
                original.DiaChi = Customer.DiaChi;
                original.MaNV = Customer.MaNV;

                return 1;
            }

            int countBefore = customers.Count;

            Customer.MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau);
            customers.Add(Customer);

            return customers.Count - countBefore;
        }

        public int LittleSave(Entities.KhachHang Customer)
        {
            int countBefore = customers.Count;

            Customer.MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau);
            customers.Add(Customer);

            return customers.Count - countBefore;
        }

        public int NewestCustomerId()
        {
            return customers.Max(x => x.MaKH);
        }

        public void UpdateOrderAmount(int CustomerId, bool Increase = true)
        {
            KhachHang Customer = Find(CustomerId);

            if (Increase)
                Customer.SoLuongDaMua += 1;
            else
                Customer.SoLuongDaMua -= 1;
        }

        public int Count
        {
            get { return customers.Count; }
        }

        public IEnumerable<Entities.KhachHang> All
        {
            get { return customers; }
        }

        public Entities.KhachHang Find(int id)
        {
            return customers.Where(x => x.MaKH == id).SingleOrDefault();
        }

        public int Save(Entities.KhachHang Entity)
        {
            return Save(Entity, false);
        }

        public int Delete(int id)
        {
            int countBefore = customers.Count;

            customers.Remove(Find(id));

            return countBefore - customers.Count;
        }
    }
}
