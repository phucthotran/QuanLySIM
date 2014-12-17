using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLySIM.Entities;
using System.Data.Common;
using System.Data.SqlClient;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public const String COL_ROLE_ID = "RoleId";
        public const String COL_NAME = "Name";

        public const String PARAM_ROLE_ID = "RoleId";
        public const String PARAM_NAME = "Name";

        private List<Role> _all = null;

        public IEnumerable<Role> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllRole");

                    if (!dbReader.HasRows)
                        return new List<Role>();

                    Role rRole;
                    _all = new List<Role>();

                    while (dbReader.Read())
                    {
                        rRole = new Role();
                        rRole.RoleId = dbReader.GetInt32(dbReader.GetOrdinal(COL_ROLE_ID));
                        rRole.Name = dbReader.GetString(dbReader.GetOrdinal(COL_NAME));

                        _all.Add(rRole);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<Role>();
            }
        }

        public RoleRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public Role Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findRoleById", new SqlParameter(PARAM_ROLE_ID, id));

                if (!dbReader.HasRows)
                    return new Role();

                dbReader.Read();

                Role rRole = new Role();
                rRole.RoleId = id;
                rRole.Name = dbReader.GetString(dbReader.GetOrdinal(COL_NAME));

                return rRole;
            }
            catch (Exception)
            {
            }

            return new Role();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public int Save(Role Entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}