using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public interface ICustomerRepository : IEntityRepository<KhachHang>
    {
        int Available(string Username, string Password);
        bool IsAccountExist(string Username);
        bool IsEmailExist(string Email);
        bool IsIdCardExist(string IdCardNumber);
        bool IsPhoneNumExist(string PhoneNum);
        int Save(KhachHang Customer, bool PasswordNotHash);
        int LittleSave(KhachHang Customer);
        int NewestCustomerId();
        void UpdateOrderAmount(int CustomerId, bool Increase = true);
    }
}
