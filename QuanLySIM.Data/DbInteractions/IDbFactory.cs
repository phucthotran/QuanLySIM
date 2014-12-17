using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySIM.Data.DbInteractions
{
    public interface IDbFactory : IDisposable
    {
        QuanLySIMContext Get();
    }
}
