using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;

namespace QuanLySIM.Tests.FakeRepositories
{
    class FakeGroupRepository : IGroupRepository
    {
        IList<Nhom> groups;
        IList<NhanVien> staffs;

        public FakeGroupRepository()
        {
            groups = new List<Nhom>
            {
                new Nhom { MaNhom = 1, Ten = "Administrator", MoTa = "Administrator Group", RoleId = 1, Role = new FakeRoleRepository().Find(1) },
                new Nhom { MaNhom = 2, Ten = "Staff", MoTa = "Staff Group", RoleId = 2, Role = new FakeRoleRepository().Find(2) },
                new Nhom { MaNhom = 3, Ten = "Staff", MoTa = "Staff Group", RoleId = 2, Role = new FakeRoleRepository().Find(2) }
            };
        }

        public IEnumerable<Entities.NhanVien> GetStaffs(int GroupId)
        {
            return new FakeStaffRepository().All.Where(x => x.MaNhom == GroupId);
        }

        public bool IsExist(string Name)
        {
            return groups.Where(x => x.Ten == Name).SingleOrDefault() != null;
        }

        public int Count
        {
            get { return groups.Count; }
        }

        public IEnumerable<Entities.Nhom> All
        {
            get { return groups; }
        }

        public Entities.Nhom Find(int id)
        {
            return groups.Where(x => x.MaNhom == id).SingleOrDefault();
        }

        public int Save(Entities.Nhom Entity)
        {
            if (Entity.MaNhom != 0)
            {
                Nhom original = Find(Entity.MaNhom);
                original.Ten = Entity.Ten;
                original.MoTa = Entity.MoTa;
                original.RoleId = Entity.RoleId;

                return 1;
            }

            int countBefore = groups.Count;

            groups.Add(Entity);

            return groups.Count - countBefore;
        }

        public int Delete(int id)
        {
            int countBefore = groups.Count;

            groups.Remove(Find(id));

            return countBefore - groups.Count;
        }
    }
}
