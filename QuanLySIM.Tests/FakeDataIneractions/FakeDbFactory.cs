using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Tests.FakeDataIneractions
{
    class FakeDbFactory : IDbFactory
    {
        public Data.QuanLySIMContext Get()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
