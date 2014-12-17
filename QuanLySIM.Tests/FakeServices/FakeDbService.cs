using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Services;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Tests.FakeServices
{
    class FakeDbService : IFakeDbService
    {
        private ICustomerRepository _customer;
        private IRoleRepository _role;
        private IGroupRepository _group;
        private IOrderRepository _order;
        private ISimRepository _sim;
        private IStaffRepository _staff;

        public FakeDbService(ICustomerRepository customer, IRoleRepository role, IGroupRepository group, ISimRepository sim, IOrderRepository order, IStaffRepository staff)
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
            set { _customer = value; }
        }

        public IStaffRepository Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        public IRoleRepository Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public IGroupRepository Group
        {
            get { return _group; }
            set { _group = value; }
        }

        public IOrderRepository Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public ISimRepository SIM
        {
            get { return _sim; }
            set { _sim = value; }
        }
    }
}
