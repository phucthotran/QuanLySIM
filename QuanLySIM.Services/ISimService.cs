using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface ISimService
    {
        IEnumerable<SIM> All { get; }
        int Count { get; }
        int Delete(int id);
        SIM Find(int id);
        IEnumerable<SIM> GetNewest();
        bool IsNumExist(string PhoneNumber);
        bool IsSerialExist(string Serial);
        int Save(SIM SIM);
    }
}
