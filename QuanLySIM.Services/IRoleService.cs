using System;
using QuanLySIM.Entities;
using System.Collections.Generic;

namespace QuanLySIM.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> All { get; }
        Role Find(int id);
    }
}
