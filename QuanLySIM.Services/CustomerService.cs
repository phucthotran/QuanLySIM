using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository CustomerRepository)
        {
            this._customerRepository = CustomerRepository;
        }

        public int Count
        {
            get { return _customerRepository.Count; }
        }

        public IEnumerable<KhachHang> All
        {
            get { return _customerRepository.All; }
        }                      

        public int Delete(int id)
        {
            return _customerRepository.Delete(id);
        }

        public int Save(KhachHang Customer)
        {
            return _customerRepository.Save(Customer);
        }

        public int Save(KhachHang Customer, bool PasswordNotHash)
        {
            return _customerRepository.Save(Customer, PasswordNotHash);
        }

        public int LittleSave(KhachHang Customer)
        {
            return _customerRepository.LittleSave(Customer);
        }

        public void UpdateOrderAmount(int CustomerId, bool Increase = true)
        {
            _customerRepository.UpdateOrderAmount(CustomerId, Increase);
        }

        public int Available(string Username, string Password)
        {
            return _customerRepository.Available(Username, Password);
        }  

        public KhachHang Find(int id)
        {
            return _customerRepository.Find(id);
        }

        public bool IsAccountExist(string Username)
        {
            return _customerRepository.IsAccountExist(Username);
        }

        public bool IsEmailExist(string Email)
        {
            return _customerRepository.IsEmailExist(Email);
        }

        public bool IsIdCardExist(string IdCardNumber)
        {
            return _customerRepository.IsIdCardExist(IdCardNumber);
        }

        public bool IsPhoneNumExist(string PhoneNum)
        {
            return _customerRepository.IsPhoneNumExist(PhoneNum);
        }

        public int NewestCustomerId()
        {
            return _customerRepository.NewestCustomerId();
        }        
    }
}
