using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public interface ISimRepository : IEntityRepository<SIM>
    {
        IEnumerable<SIM> GetNewest();
        bool IsNumExist(string PhoneNumber);
        bool IsSerialExist(string Serial);
    }
}
