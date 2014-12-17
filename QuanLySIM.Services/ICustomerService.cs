using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface ICustomerService
    {
        IEnumerable<KhachHang> All { get; }
        int Available(string Username, string Password);
        int Count { get; }
        int Delete(int id);
        KhachHang Find(int id);
        bool IsAccountExist(string Username);
        bool IsEmailExist(string Email);
        bool IsIdCardExist(string IdCardNumber);
        bool IsPhoneNumExist(string PhoneNum);
        int LittleSave(KhachHang Customer);
        int NewestCustomerId();
        int Save(KhachHang Customer);
        int Save(KhachHang Customer, bool PasswordNotHash);
        void UpdateOrderAmount(int CustomerId, bool Increase = true);
    }
}
