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
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public const String COL_MA_PM = "MaPM";
        public const String COL_NGAY_DAT = "NgayDatMua";
        public const String COL_NGAY_GIAO = "NgayGiao";
        public const String COL_TONG_TIEN = "TongTien";
        public const String COL_MA_KH = "MaKH";
        public const String COL_SIM_ID = "SimId";

        public const String PARAM_MA_PM = "MaPM";
        public const String PARAM_NGAY_DAT = "NgayDatMua";
        public const String PARAM_NGAY_GIAO = "NgayGiao";
        public const String PARAM_TONG_TIEN = "TongTien";
        public const String PARAM_MA_KH = "MaKH";
        public const String PARAM_SIM_ID = "SimId";

        private int _count = 0;
        private List<PhieuMua> _all = null;

        public int Count
        {
            get
            {
                try
                {
                    _count = (int)DbContext.ScalarRecord("countPhieuMua");
                    return _count;
                }
                catch (Exception)
                {
                }

                return 0;
            }
        }

        public IEnumerable<PhieuMua> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllPhieuMua");

                    if (!dbReader.HasRows)
                        return new List<PhieuMua>();

                    PhieuMua rOrder;
                    _all = new List<PhieuMua>();

                    while (dbReader.Read())
                    {
                        rOrder = new PhieuMua();
                        rOrder.MaPM = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_PM));
                        rOrder.NgayDatMua = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_DAT));
                        rOrder.NgayGiao = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_GIAO));
                        rOrder.TongTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_TONG_TIEN));
                        rOrder.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                        rOrder.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                        rOrder.KhachHang = new CustomerRepository(DbFactory).Find(rOrder.MaKH);
                        rOrder.SIM = new SimRepository(DbFactory).Find(rOrder.SimId);

                        _all.Add(rOrder);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<PhieuMua>();
            }
        }

        public OrderRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public PhieuMua Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaById", new SqlParameter(PARAM_MA_PM, id));

                if (!dbReader.HasRows)
                    return new PhieuMua();

                dbReader.Read();

                PhieuMua rOrder = new PhieuMua();
                rOrder.MaPM = id;
                rOrder.NgayDatMua = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_DAT));
                rOrder.NgayGiao = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_GIAO));
                rOrder.TongTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_TONG_TIEN));
                rOrder.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                rOrder.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                rOrder.KhachHang = new CustomerRepository(DbFactory).Find(rOrder.MaKH);
                rOrder.SIM = new SimRepository(DbFactory).Find(rOrder.SimId);

                return rOrder;
            }
            catch (Exception)
            {
            }

            return new PhieuMua();
        }

        public int Save(PhieuMua Order)
        {
            try
            {
                if (Order.MaPM != 0)
                {
                    return DbContext.NonRecord("updatePhieuMua",
                                    new SqlParameter(PARAM_MA_PM, Order.MaPM),
                                    new SqlParameter(PARAM_NGAY_DAT, Order.NgayDatMua),
                                    new SqlParameter(PARAM_NGAY_GIAO, Order.NgayGiao),
                                    new SqlParameter(PARAM_TONG_TIEN, Order.TongTien),
                                    new SqlParameter(PARAM_SIM_ID, Order.SimId)
                                    );
                }
                //else
                return DbContext.NonRecord("insertPhieuMua",
                                    new SqlParameter(PARAM_NGAY_DAT, Order.NgayDatMua),
                                    new SqlParameter(PARAM_NGAY_GIAO, Order.NgayGiao),
                                    new SqlParameter(PARAM_TONG_TIEN, Order.TongTien),
                                    new SqlParameter(PARAM_MA_KH, Order.MaKH),
                                    new SqlParameter(PARAM_SIM_ID, Order.SimId)
                                    );
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public int LittleSave(PhieuMua Order)
        {
            try
            {
                return DbContext.NonRecord("insertPhieuMuaLittle",
                                    new SqlParameter(PARAM_MA_KH, Order.MaKH),
                                    new SqlParameter(PARAM_SIM_ID, Order.SimId)
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
                return DbContext.NonRecord("deletePhieuMua", new SqlParameter(PARAM_MA_PM, id));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public int DeleteByCustomer(int CustomerId)
        {
            try
            {
                return DbContext.NonRecord("deletePhieuMuaByMaKH", new SqlParameter(PARAM_MA_KH, CustomerId));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public bool IsOrdered(int SimId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaBySimId", new SqlParameter(PARAM_SIM_ID, SimId));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsTheSame(int CustomerId, int SimId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaByMaKHandSimId",
                        new SqlParameter(PARAM_MA_KH, CustomerId),
                        new SqlParameter(PARAM_SIM_ID, SimId)
                    );

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsCustomerOutOfOrderTimes(int CustomerId)
        {
            try
            {
                int orderTimes = 0;

                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaOrderdByMaKH",
                        new SqlParameter(PARAM_MA_KH, CustomerId),
                        new SqlParameter(SimRepository.PARAM_TINH_TRANG, SIM.NOT_PAID)
                    );

                if (!dbReader.HasRows)
                    return false;

                while (dbReader.Read())
                    orderTimes++;

                return orderTimes >= 2;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IEnumerable<SIM> FindCustomerOrderSIMs(int CustomerId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaOrderdByMaKH",
                                                    new SqlParameter(PARAM_MA_KH, CustomerId),
                                                    new SqlParameter(SimRepository.PARAM_TINH_TRANG, String.Empty)
                                                    );

                if (!dbReader.HasRows)
                    return new List<SIM>();

                SIM rSIM;
                List<SIM> rsSIMs = new List<SIM>();

                while (dbReader.Read())
                {
                    rSIM = new SIM();
                    rSIM.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));

                    SIM tmp = new SimRepository(DbFactory).Find(rSIM.SimId);

                    rSIM.SoThueBao = tmp.SoThueBao;
                    rSIM.TinhTrang = tmp.TinhTrang;

                    rsSIMs.Add(rSIM);
                }

                return rsSIMs;
            }
            catch (Exception)
            {
            }

            return new List<SIM>();
        }

        public IEnumerable<PhieuMua> FindOrdersByCustomer(int CustomerId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaOrderdByMaKH",
                                                    new SqlParameter(PARAM_MA_KH, CustomerId),
                                                    new SqlParameter(SimRepository.PARAM_TINH_TRANG, "")
                                                    );

                if (!dbReader.HasRows)
                    return new List<PhieuMua>();

                PhieuMua rOrder;
                List<PhieuMua> rsOrders = new List<PhieuMua>();

                while (dbReader.Read())
                {
                    rOrder = new PhieuMua();
                    rOrder.MaPM = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_PM));
                    rOrder.NgayDatMua = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_DAT));
                    rOrder.NgayGiao = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_GIAO));
                    rOrder.TongTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_TONG_TIEN));
                    rOrder.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                    rOrder.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                    rOrder.KhachHang = new CustomerRepository(DbFactory).Find(rOrder.MaKH);
                    rOrder.SIM = new SimRepository(DbFactory).Find(rOrder.SimId);

                    rsOrders.Add(rOrder);
                }

                return rsOrders;
            }
            catch (Exception)
            {
            }

            return new List<PhieuMua>();
        }


        public PhieuMua FindOrderOfCustomerBySIM(int CustomerId, int SimId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findPhieuMuaByMaKHandSimId",
                                                    new SqlParameter(PARAM_MA_KH, CustomerId),
                                                    new SqlParameter(PARAM_SIM_ID, SimId)
                                                    );

                if (!dbReader.HasRows)
                    return new PhieuMua();

                dbReader.Read();

                PhieuMua rOrder = new PhieuMua();
                rOrder.MaPM = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_PM));
                rOrder.NgayDatMua = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_DAT));
                rOrder.NgayGiao = dbReader.GetDateTime(dbReader.GetOrdinal(COL_NGAY_GIAO));
                rOrder.TongTien = dbReader.GetDecimal(dbReader.GetOrdinal(COL_TONG_TIEN));
                rOrder.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                rOrder.SimId = SimId;
                rOrder.KhachHang = new CustomerRepository(DbFactory).Find(rOrder.MaKH);
                rOrder.SIM = new SimRepository(DbFactory).Find(rOrder.SimId);

                return rOrder;
            }
            catch (Exception)
            {
            }

            return new PhieuMua();
        }

        public IEnumerable<SIM> FindSIMOrderInSevenDays()
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findAllPhieuMuaOrderInWeek");

                if (!dbReader.HasRows)
                    return new List<SIM>();

                SIM rSIM;
                List<SIM> rsSIMs = new List<SIM>();

                while (dbReader.Read())
                {
                    rSIM = new SIM();
                    rSIM.SimId = dbReader.GetInt32(dbReader.GetOrdinal(COL_SIM_ID));
                    rSIM.SoThueBao = dbReader.GetString(dbReader.GetOrdinal("SoThueBao"));

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