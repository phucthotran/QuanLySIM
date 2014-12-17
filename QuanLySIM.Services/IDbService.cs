using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public interface IDbService
    {
        ICustomerRepository Customer { get; }
        IStaffRepository Staff { get; }
        IRoleRepository Role { get; }
        IGroupRepository Group { get; }
        IOrderRepository Order { get; }
        ISimRepository SIM { get; }
    }
}
