using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public interface IGroupRepository : IEntityRepository<Nhom>
    {
        IEnumerable<NhanVien> GetStaffs(int GroupId);
        bool IsExist(string Name);
    }
}
