using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository RoleRepository)
        {
            this._roleRepository = RoleRepository;
        }

        public IEnumerable<Role> All
        {
            get { return _roleRepository.All; }
        }

        public Role Find(int id)
        {
            return _roleRepository.Find(id);
        }
    }
}
