using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySIM.Data.DbInteractions
{
    public interface IEntityRepository<E> where E : class
    {
        int Count { get; }
        IEnumerable<E> All { get; }
        E Find(int id);
        int Save(E Entity);
        int Delete(int id);
    }
}
