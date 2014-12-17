using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLySIM.Data.DbInteractions
{
    public class DbFactory : Disposable, IDbFactory
    {
        private QuanLySIMContext dbContext;

        public QuanLySIMContext Get()
        {
            if (dbContext == null)
                return dbContext = new QuanLySIMContext();

            if (dbContext.Database.Connection.State == System.Data.ConnectionState.Closed)
                dbContext.Database.Connection.Open();
                        
            return dbContext;
        }

        public new void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
