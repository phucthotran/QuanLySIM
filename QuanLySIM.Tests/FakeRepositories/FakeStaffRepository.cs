using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    class FakeStaffRepository : IStaffRepository
    {
        IList<NhanVien> staffs;

        public FakeStaffRepository()
        {
            staffs = new List<NhanVien>
            {
                new NhanVien { MaNV = 1, MaNhom = 1, TenTK = "staff1234", MatKhau = "pass1234", Email = "email1@gmail.com", TenNV = "Nhan Vien 1", DiaChi = "Dia Chi 1", CMND = "111111111", SDT = "011111111", Nhom = new FakeGroupRepository().Find(1) },
                new NhanVien { MaNV = 2, MaNhom = 2, TenTK = "staff2234", MatKhau = "pass2234", Email = "email2@gmail.com", TenNV = "Nhan Vien 2", DiaChi = "Dia Chi 2", CMND = "211111111", SDT = "021111111", Nhom = new FakeGroupRepository().Find(2) },
                new NhanVien { MaNV = 3, MaNhom = 2, TenTK = "staff2234", MatKhau = "pass2234", Email = "email2@gmail.com", TenNV = "Nhan Vien 3", DiaChi = "Dia Chi 3", CMND = "211111111", SDT = "021111111", Nhom = new FakeGroupRepository().Find(2) }
            };
        }

        public int Available(string Username, string Password, out string Role)
        {
            NhanVien s = staffs.Where(x => x.TenTK == Username && x.MatKhau == Password).SingleOrDefault();
            Role = String.Empty;

            if (s == null)
                return 0;

            Role = s.Nhom.Role.Name;
            
            return s.MaNV;
        }

        public IEnumerable<Entities.KhachHang> GetCustomers(int StaffId)
        {
            return new FakeCustomerRepository().All.Where(x => x.MaNV == StaffId);
        }

        public IEnumerable<int> GetStaffIds()
        {
            return staffs.Select<NhanVien, int>(x => x.MaNV);
        }

        public bool IsAccountExist(string Username)
        {
            return staffs.Where(x => x.TenTK == Username).SingleOrDefault() != null;
        }

        public bool IsEmailExist(string Email)
        {
            return staffs.Where(x => x.Email == Email).SingleOrDefault() != null;
        }

        public bool IsIdCardExist(string IdCardNumber)
        {
            return staffs.Where(x => x.CMND == IdCardNumber).SingleOrDefault() != null;
        }

        public bool IsPhoneNumExist(string PhoneNum)
        {
            return staffs.Where(x => x.SDT == PhoneNum).SingleOrDefault() != null;
        }

        public int Save(Entities.NhanVien Staff, bool PasswordNotHash)
        {
            if (Staff.MaNV != 0)
            {
                NhanVien original = Find(Staff.MaNV);
                original.TenTK = Staff.TenTK;
                original.MatKhau = PasswordNotHash == true ? Staff.MatKhau : Lib.Md5Sha1Encrypt.SHA1Hashing(Staff.MatKhau);
                original.Email = Staff.Email;
                original.TenNV = Staff.TenNV;
                original.DiaChi = Staff.DiaChi;
                original.CMND = Staff.CMND;
                original.SDT = Staff.SDT;
                original.MaNhom = Staff.MaNhom;
                                
                return 1;
            }

            int countBefore = staffs.Count;

            Staff.MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing(Staff.MatKhau);
            staffs.Add(Staff);

            return staffs.Count - countBefore;
        }

        public int Count
        {
            get { return staffs.Count; }
        }

        public IEnumerable<Entities.NhanVien> All
        {
            get { return staffs; }
        }

        public Entities.NhanVien Find(int id)
        {
            return staffs.Where(x => x.MaNV == id).SingleOrDefault();
        }

        public int Save(Entities.NhanVien Entity)
        {
            return Save(Entity, false);
        }

        public int Delete(int id)
        {
            int countBefore = staffs.Count;

            staffs.Remove(Find(id));

            return countBefore - staffs.Count;
        }
    }
}
