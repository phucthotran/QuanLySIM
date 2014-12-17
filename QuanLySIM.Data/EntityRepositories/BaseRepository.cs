using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using QuanLySIM.Data.DbInteractions;
using System.Data.SqlClient;

namespace QuanLySIM.Data.EntityRepositories
{
    public class BaseRepository : Disposable
    {
        private QuanLySIMContext dbContext;
        private DbConnection dbConnection;

        protected IDbFactory DbFactory { get; private set; }

        protected QuanLySIMContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Get()); }
        }

        public BaseRepository(IDbFactory DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();

            if (DbFactory != null)
                DbFactory.Dispose();
        }
    }
}
