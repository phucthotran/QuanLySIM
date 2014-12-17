using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface IGroupService
    {
        IEnumerable<Nhom> All { get; }
        int Count { get; }
        int Delete(int id);
        Nhom Find(int id);
        IEnumerable<NhanVien> GetStaffs(int GroupId);
        bool IsExist(string Name);
        int Save(Nhom Group);
    }
}
