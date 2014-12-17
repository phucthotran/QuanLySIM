using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class DbService : IDbService
    {
        private readonly ICustomerRepository _customer;
        private readonly IRoleRepository _role;
        private readonly IGroupRepository _group;
        private readonly IOrderRepository _order;
        private readonly ISimRepository _sim;
        private readonly IStaffRepository _staff;

        public DbService(ICustomerRepository customer, IRoleRepository role, IGroupRepository group, ISimRepository sim, IOrderRepository order, IStaffRepository staff)
        {
            this._customer = customer;
            this._role = role;
            this._group = group;
            this._sim = sim;
            this._staff = staff;
            this._order = order;
        }

        public ICustomerRepository Customer
        {
            get { return _customer; }
        }

        public IStaffRepository Staff
        {
            get { return _staff; }
        }

        public IRoleRepository Role
        {
            get { return _role; }
        }

        public IGroupRepository Group
        {
            get { return _group; }
        }

        public IOrderRepository Order
        {
            get { return _order; }
        }

        public ISimRepository SIM
        {
            get { return _sim; }
        }
    }
}
