using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface IStaffService
    {
        IEnumerable<NhanVien> All { get; }
        int Available(string Username, string Password, out string Role);
        int Count { get; }
        int Delete(int id);
        NhanVien Find(int id);
        IEnumerable<KhachHang> GetCustomers(int StaffId);
        IEnumerable<int> GetStaffIds();
        bool IsAccountExist(string Username);
        bool IsEmailExist(string Email);
        bool IsIdCardExist(string IdCardNumber);
        bool IsPhoneNumExist(string PhoneNum);
        int Save(NhanVien Staff);
        int Save(NhanVien Staff, bool PasswordNotHash);
    }
}
