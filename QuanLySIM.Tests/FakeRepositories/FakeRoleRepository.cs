using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    class FakeRoleRepository : IRoleRepository
    {
        IList<Role> roles;

        public FakeRoleRepository()
        {
            roles = new List<Role>
            {
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "Staff" }
            };
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Entities.Role> All
        {
            get { return roles; }
        }

        public Entities.Role Find(int id)
        {
            return roles.Where(x => x.RoleId == id).SingleOrDefault();
        }

        public int Save(Entities.Role Entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
