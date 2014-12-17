using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLySIM.Data;
using System.Data.Common;
using QuanLySIM.Entities;
using System.Data.SqlClient;
using QuanLySIM.Data.DbInteractions;

namespace QuanLySIM.Data.EntityRepositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public const String COL_MA_KH = "MaKH";
        public const String COL_TEN_TK = "TenTK";
        public const String COL_MAT_KHAU = "MatKhau";
        public const String COL_EMAIL = "Email";
        public const String COL_TEN_KH = "TenKH";
        public const String COL_CMND = "CMND";
        public const String COL_DIA_CHI = "DiaChi";
        public const String COL_SDT = "SDT";
        public const String COL_SO_LUONG = "SoLuongDaMua";
        public const String COL_MA_NV = "MaNV";

        public const String PARAM_MA_KH = "MaKH";
        public const String PARAM_TEN_TK = "TenTK";
        public const String PARAM_MAT_KHAU = "MatKhau";
        public const String PARAM_EMAIL = "Email";
        public const String PARAM_TEN_KH = "TenKH";
        public const String PARAM_CMND = "CMND";
        public const String PARAM_DIA_CHI = "DiaChi";
        public const String PARAM_SDT = "SDT";
        public const String PARAM_SO_LUONG = "SoLuongMua";
        public const String PARAM_MA_NV = "MaNV";

        private int _count = 0;
        private List<KhachHang> _all = null;

        public int Count
        {
            get
            {
                try
                {
                    _count = (int)DbContext.ScalarRecord("countKhachHang");
                    return _count;
                }
                catch (Exception)
                {
                }

                return 0;
            }
        }

        public IEnumerable<KhachHang> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllKhachHang");

                    if (!dbReader.HasRows)
                        return new List<KhachHang>();

                    KhachHang rCustomer;
                    _all = new List<KhachHang>();

                    while (dbReader.Read())
                    {
                        rCustomer = new KhachHang();

                        rCustomer.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                        rCustomer.TenTK = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_TK));
                        rCustomer.MatKhau = dbReader.GetString(dbReader.GetOrdinal(COL_MAT_KHAU));
                        rCustomer.Email = dbReader.GetString(dbReader.GetOrdinal(COL_EMAIL));
                        rCustomer.TenKH = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_KH));
                        rCustomer.CMND = dbReader.GetString(dbReader.GetOrdinal(COL_CMND));
                        rCustomer.DiaChi = dbReader.GetString(dbReader.GetOrdinal(COL_DIA_CHI));
                        rCustomer.SDT = dbReader.GetString(dbReader.GetOrdinal(COL_SDT));
                        rCustomer.SoLuongDaMua = dbReader.GetInt32(dbReader.GetOrdinal(COL_SO_LUONG));
                        rCustomer.MaNV = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NV));
                        rCustomer.NhanVien = new StaffRepository(DbFactory).Find(rCustomer.MaNV);

                        _all.Add(rCustomer);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<KhachHang>();
            }
        }

        public CustomerRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public KhachHang Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findKhachHangById", new SqlParameter(PARAM_MA_KH, id));

                if (!dbReader.HasRows)
                    return new KhachHang();

                dbReader.Read();

                KhachHang rCustomer = new KhachHang();
                rCustomer.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
                rCustomer.TenTK = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_TK));
                rCustomer.MatKhau = dbReader.GetString(dbReader.GetOrdinal(COL_MAT_KHAU));
                rCustomer.Email = dbReader.GetString(dbReader.GetOrdinal(COL_EMAIL));
                rCustomer.TenKH = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_KH));
                rCustomer.CMND = dbReader.GetString(dbReader.GetOrdinal(COL_CMND));
                rCustomer.DiaChi = dbReader.GetString(dbReader.GetOrdinal(COL_DIA_CHI));
                rCustomer.SDT = dbReader.GetString(dbReader.GetOrdinal(COL_SDT));
                rCustomer.SoLuongDaMua = dbReader.GetInt32(dbReader.GetOrdinal(COL_SO_LUONG));
                rCustomer.SoLuongDangDatMua = new OrderRepository(DbFactory).FindCustomerOrderSIMs(rCustomer.MaKH).Count();
                rCustomer.MaNV = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NV));
                rCustomer.NhanVien = new StaffRepository(DbFactory).Find(rCustomer.MaNV);

                return rCustomer;
            }
            catch (Exception)
            {
            }

            return new KhachHang();
        }

        public int Save(KhachHang Customer)
        {
            return Save(Customer, false);
        }

        public int Save(KhachHang Customer, bool PasswordNotHash)
        {
            try
            {
                if (Customer.MaKH != 0)
                {
                    return DbContext.NonRecord("updateKhachHang",
                                    new SqlParameter(PARAM_MA_KH, Customer.MaKH),
                                    new SqlParameter(PARAM_TEN_TK, Customer.TenTK),
                                    new SqlParameter(PARAM_MAT_KHAU, PasswordNotHash == true ? Customer.MatKhau : Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau)),
                                    new SqlParameter(PARAM_EMAIL, Customer.Email),
                                    new SqlParameter(PARAM_TEN_KH, Customer.TenKH),
                                    new SqlParameter(PARAM_CMND, Customer.CMND),
                                    new SqlParameter(PARAM_DIA_CHI, Customer.DiaChi),
                                    new SqlParameter(PARAM_SDT, Customer.SDT),
                                    new SqlParameter(PARAM_MA_NV, Customer.MaNV)
                                    );
                }
                //else
                return DbContext.NonRecord("insertKhachHang",
                                    new SqlParameter(PARAM_TEN_TK, Customer.TenTK),
                                    new SqlParameter(PARAM_MAT_KHAU, Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau)),
                                    new SqlParameter(PARAM_EMAIL, Customer.Email),
                                    new SqlParameter(PARAM_TEN_KH, Customer.TenKH),
                                    new SqlParameter(PARAM_CMND, Customer.CMND),
                                    new SqlParameter(PARAM_DIA_CHI, Customer.DiaChi),
                                    new SqlParameter(PARAM_SDT, Customer.SDT),
                                    new SqlParameter(PARAM_MA_NV, Customer.MaNV)
                                    );
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public void UpdateOrderAmount(int CustomerId, bool Increase = true)
        {
            try
            {
                KhachHang Customer = Find(CustomerId);
                if (Increase)
                    Customer.SoLuongDaMua += 1;
                else
                    Customer.SoLuongDaMua -= 1;

                DbContext.NonRecord("updateKhachHangOrderAmount",
                            new SqlParameter(PARAM_MA_KH, Customer.MaKH),
                            new SqlParameter(PARAM_SO_LUONG, Customer.SoLuongDaMua)
                            );
            }
            catch (Exception)
            {
            }
        }

        public int LittleSave(KhachHang Customer)
        {
            try
            {
                return DbContext.NonRecord("insertLittleKhachHang",
                                    new SqlParameter(PARAM_TEN_TK, Customer.TenTK),
                                    new SqlParameter(PARAM_MAT_KHAU, Lib.Md5Sha1Encrypt.SHA1Hashing(Customer.MatKhau)),
                                    new SqlParameter(PARAM_EMAIL, Customer.Email),
                                    new SqlParameter(PARAM_MA_NV, Customer.MaNV)
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
                return DbContext.NonRecord("deleteKhachHang", new SqlParameter(PARAM_MA_KH, id));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public int Available(String Username, String Password)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("loginKhachHang",
                                                    new SqlParameter(PARAM_TEN_TK, Username),
                                                    new SqlParameter(PARAM_MAT_KHAU, Lib.Md5Sha1Encrypt.SHA1Hashing(Password))
                                                    );

                if (!dbReader.HasRows)
                    return 0;

                dbReader.Read();

                return dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_KH));
            }
            catch (Exception)
            {
            }

            return 0;
        }

        public bool IsAccountExist(String Username)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findKhachHangByTenTK", new SqlParameter(PARAM_TEN_TK, Username));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsEmailExist(String Email)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findKhachHangByEmail", new SqlParameter(PARAM_EMAIL, Email));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsIdCardExist(String IdCardNumber)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findKhachHangByCMND", new SqlParameter(PARAM_CMND, IdCardNumber));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool IsPhoneNumExist(String PhoneNum)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findKhachHangBySDT", new SqlParameter(PARAM_SDT, PhoneNum));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public int NewestCustomerId()
        {
            try
            {
                return (int)DbContext.ScalarRecord("findLastKhachHangMaKH");
            }
            catch (Exception)
            {
            }

            return -1;
        }
    }
}