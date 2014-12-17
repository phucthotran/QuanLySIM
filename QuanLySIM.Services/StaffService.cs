using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository StaffRepository)
        {
            this._staffRepository = StaffRepository;
        }

        public int Count
        {
            get { return _staffRepository.Count; }
        }

        public IEnumerable<NhanVien> All
        {
            get { return _staffRepository.All; }
        }

        public int Delete(int id)
        {
            return _staffRepository.Delete(id);
        }

        public int Save(NhanVien Staff)
        {
            return _staffRepository.Save(Staff);
        }

        public int Save(NhanVien Staff, bool PasswordNotHash)
        {
            return _staffRepository.Save(Staff, PasswordNotHash);
        }

        public int Available(string Username, string Password, out string Role)
        {
            return _staffRepository.Available(Username, Password, out Role);
        }                

        public NhanVien Find(int id)
        {
            return _staffRepository.Find(id);
        }

        public IEnumerable<KhachHang> GetCustomers(int StaffId)
        {
            return _staffRepository.GetCustomers(StaffId);
        }

        public IEnumerable<int> GetStaffIds()
        {
            return _staffRepository.GetStaffIds();
        }

        public bool IsAccountExist(string Username)
        {
            return _staffRepository.IsAccountExist(Username);
        }

        public bool IsEmailExist(string Email)
        {
            return _staffRepository.IsEmailExist(Email);
        }

        public bool IsIdCardExist(string IdCardNumber)
        {
            return _staffRepository.IsIdCardExist(IdCardNumber);
        }

        public bool IsPhoneNumExist(string PhoneNum)
        {
            return _staffRepository.IsPhoneNumExist(PhoneNum);
        }
    }
}
