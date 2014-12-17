using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using QuanLySIM.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace QuanLySIM.Data
{
    public class QuanLySIMContext : DbContext
    {
        public QuanLySIMContext() : base("QuanLySIMConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            //Create New Database If Not Exist
            Database.SetInitializer<QuanLySIMContext>(new CreateDatabaseIfNotExists<QuanLySIMContext>());
        }
    
        public DbSet<NhanVien> Staff { get; set; }
        public DbSet<KhachHang> Customer { get; set; }
        public DbSet<PhieuMua> Order { get; set; }
        public DbSet<SIM> SIM { get; set; }
        public DbSet<Nhom> Group { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbDataReader RecordSets(String ProcedureName, params SqlParameter[] Parameters)
        {
            try
            {
                if (Database.Connection.State == System.Data.ConnectionState.Closed)
                    Database.Connection.Open();

                DbCommand dbCmd = Database.Connection.CreateCommand();
                dbCmd.CommandText = ProcedureName;
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameterCollection dbParams = dbCmd.Parameters;

                foreach (SqlParameter parameter in Parameters)
                    dbParams.Add(parameter);

                return dbCmd.ExecuteReader();
            }
            catch (SqlException)
            {
            }

            return null;
        }

        public Object ScalarRecord(String ProcedureName, params SqlParameter[] Parameters)
        {
            try
            {
                if (Database.Connection.State == System.Data.ConnectionState.Closed)
                    Database.Connection.Open();

                DbCommand dbCmd = Database.Connection.CreateCommand();
                dbCmd.CommandText = ProcedureName;
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameterCollection dbParams = dbCmd.Parameters;

                foreach (SqlParameter parameter in Parameters)
                    dbParams.Add(parameter);

                return dbCmd.ExecuteScalar();
            }
            catch (SqlException)
            {
            }

            return null;
        }

        public int NonRecord(String ProcedureName, params SqlParameter[] Parameters)
        {
            try
            {
                if (Database.Connection.State == System.Data.ConnectionState.Closed)
                    Database.Connection.Open();

                DbCommand dbCmd = Database.Connection.CreateCommand();
                dbCmd.CommandText = ProcedureName;
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                DbParameterCollection dbParams = dbCmd.Parameters;

                foreach (SqlParameter parameter in Parameters)
                    dbParams.Add(parameter);

                return dbCmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
            }

            return -1;
        }
    }
}