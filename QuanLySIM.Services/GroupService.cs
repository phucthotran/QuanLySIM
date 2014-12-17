using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Entities;
using QuanLySIM.Data.EntityRepositories;

namespace QuanLySIM.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository GroupRepository)
        {
            this._groupRepository = GroupRepository;
        }

        public int Count
        {
            get { return _groupRepository.Count; }
        }

        public IEnumerable<Nhom> All
        {
            get { return _groupRepository.All; }
        }        

        public int Delete(int id)
        {
            return _groupRepository.Delete(id);
        }

        public int Save(Nhom Group)
        {
            return _groupRepository.Save(Group);
        }

        public Nhom Find(int id)
        {
            return _groupRepository.Find(id);
        }

        public IEnumerable<NhanVien> GetStaffs(int GroupId)
        {
            return _groupRepository.GetStaffs(GroupId);
        }

        public bool IsExist(string Name)
        {
            return _groupRepository.IsExist(Name);
        }
    }
}
