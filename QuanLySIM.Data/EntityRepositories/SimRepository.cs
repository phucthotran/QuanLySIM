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
    public class SimRepository : BaseRepository, ISimRepository
    {
        public const String COL_SIM_ID = "SimId";
        public const String COL_MA_SIM = "MaSIM";
        public const String COL_SO_THUE_BAO = "SoThueBao";
        public const String COL_GIA_TIEN = "GiaTien";
        public const String COL_TINH_TRANG = "TinhTrang";

        public const String PARAM_SIM_ID = "SimId";
        public const String PARAM_MA_SIM = "MaSIM";
        public const String PARAM_SO_THUE_BAO = "SoThueBao";
        public const String PARAM_GIA_TIEN = "GiaTien";
        public const String PARAM_TINH_TRANG = "TinhTrang";

        private int _count = 0;
        private List<SIM> _all = null;

        public int Count
        {
            get
            {
                try
                {
                    _count = (int)DbContext.ScalarRecord("countSIM");
                    return _count;
                }
                catch (Exception)
                {
                }

                return 0;
            }
        }

        public IEnumerable<SIM> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllSIM");

                    if (!dbReader.HasRows)
                        return new List<SIM>();

                    SIM rSIM;
                    _all = new List<SIM>();

                    while (dbReader.Read())
                    {
                        rSIM = new SIM();
                        rSIM.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                        rSIM.MaSIM = dbReader.GetString(dbReader.GetOrdinal(COL_MA_SIM));
                        rSIM.SoThueBao = dbReader.GetString(dbReader.GetOrdinal(COL_SO_THUE_BAO));
                        rSIM.GiaTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_GIA_TIEN));
                        rSIM.TinhTrang = dbReader.GetString(dbReader.GetOrdinal(COL_TINH_TRANG));

                        _all.Add(rSIM);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<SIM>();
            }
        }

        public SimRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public SIM Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findSIMById", new SqlParameter(COL_SIM_ID, id));

                if (!dbReader.HasRows)
                    return new SIM();

                dbReader.Read();

                SIM rSIM = new SIM();
                rSIM.SimId = id;
                rSIM.MaSIM = dbReader.GetString(dbReader.GetOrdinal(COL_MA_SIM));
                rSIM.SoThueBao = dbReader.GetString(dbReader.GetOrdinal(COL_SO_THUE_BAO));
                rSIM.GiaTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_GIA_TIEN));
                rSIM.TinhTrang = dbReader.GetString(dbReader.GetOrdinal(COL_TINH_TRANG));

                return rSIM;
            }
            catch (Exception)
            {
            }

            return new SIM();
        }

        public int Save(SIM SIM)
        {
            try
            {
                if (SIM.SimId != 0)
                {
                    return DbContext.NonRecord("updateSIM",
                                    new SqlParameter(PARAM_SIM_ID, SIM.SimId),
                                    new SqlParameter(PARAM_GIA_TIEN, SIM.GiaTien),
                                    new SqlParameter(PARAM_TINH_TRANG, SIM.TinhTrang)
                                    );
                }
                //else
                return DbContext.NonRecord("insertSIM",
                                    new SqlParameter(PARAM_MA_SIM, SIM.MaSIM),
                                    new SqlParameter(PARAM_SO_THUE_BAO, SIM.SoThueBao),
                                    new SqlParameter(PARAM_GIA_TIEN, SIM.GiaTien),
                                    new SqlParameter(PARAM_TINH_TRANG, SIM.TinhTrang)
                                    );
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public int Delete(int id)
        {
            try
            {
                return DbContext.NonRecord("deleteSIM", new SqlParameter(PARAM_SIM_ID, id));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public bool IsSerialExist(String Serial)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findSIMByMaSIM", new SqlParameter(PARAM_MA_SIM, Serial));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsNumExist(String PhoneNumber)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findSIMBySoThueBao", new SqlParameter(PARAM_SO_THUE_BAO, PhoneNumber));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IEnumerable<SIM> GetNewest()
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findNewestSIM");

                if (!dbReader.HasRows)
                    return new List<SIM>();

                SIM rSIM;
                List<SIM> rsSIMs = new List<SIM>();

                while (dbReader.Read())
                {
                    rSIM = new SIM();
                    rSIM.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                    rSIM.MaSIM = dbReader.GetString(dbReader.GetOrdinal(COL_MA_SIM));
                    rSIM.SoThueBao = dbReader.GetString(dbReader.GetOrdinal(COL_SO_THUE_BAO));
                    rSIM.GiaTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_GIA_TIEN));
                    rSIM.TinhTrang = dbReader.GetString(dbReader.GetOrdinal(COL_TINH_TRANG));

                    rsSIMs.Add(rSIM);
                }

                return rsSIMs;
            }
            catch (Exception)
            {
            }

            return new List<SIM>();
        }
    }
}