using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public interface IStaffRepository : IEntityRepository<NhanVien>
    {        
        int Available(string Username, string Password, out string Role);
        IEnumerable<KhachHang> GetCustomers(int StaffId);
        IEnumerable<int> GetStaffIds();
        bool IsAccountExist(string Username);
        bool IsEmailExist(string Email);
        bool IsIdCardExist(string IdCardNumber);
        bool IsPhoneNumExist(string PhoneNum);
        int Save(NhanVien Staff, bool PasswordNotHash);
    }
}
