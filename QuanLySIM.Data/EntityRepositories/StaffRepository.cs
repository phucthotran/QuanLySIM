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
    public class StaffRepository : BaseRepository, IStaffRepository
    {
        public const String COL_MA_NV = "MaNV";
        public const String COL_TEN_TK = "TenTK";
        public const String COL_MAT_KHAU = "MatKhau";
        public const String COL_EMAIL = "Email";
        public const String COL_TEN_NV = "TenNV";
        public const String COL_CMND = "CMND";
        public const String COL_DIA_CHI = "DiaChi";
        public const String COL_SDT = "SDT";
        public const String COL_MA_NHOM = "MaNhom";

        public const String PARAM_MA_NV = "MaNV";
        public const String PARAM_TEN_TK = "TenTK";
        public const String PARAM_MAT_KHAU = "MatKhau";
        public const String PARAM_EMAIL = "Email";
        public const String PARAM_TEN_NV = "TenNV";
        public const String PARAM_CMND = "CMND";
        public const String PARAM_DIA_CHI = "DiaChi";
        public const String PARAM_SDT = "SDT";
        public const String PARAM_MA_NHOM = "MaNhom";

        private int _count = 0;
        private List<NhanVien> _all = null;

        public int Count
        {
            get
            {
                try
                {
                    _count = (int)DbContext.ScalarRecord("countNhanVien");
                    return _count;
                }
                catch (Exception)
                {
                }

                return 0;
            }
        }

        public IEnumerable<NhanVien> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllNhanVien");

                    if (!dbReader.HasRows)
                        return new List<NhanVien>();

                    NhanVien rStaff;
                    _all = new List<NhanVien>();

                    while (dbReader.Read())
                    {
                        rStaff = new NhanVien();
                        rStaff.MaNV = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NV));
                        rStaff.TenTK = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_TK));
                        rStaff.MatKhau = dbReader.GetString(dbReader.GetOrdinal(COL_MAT_KHAU));
                        rStaff.Email = dbReader.GetString(dbReader.GetOrdinal(COL_EMAIL));
                        rStaff.TenNV = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_NV));
                        rStaff.CMND = dbReader.GetString(dbReader.GetOrdinal(COL_CMND));
                        rStaff.DiaChi = dbReader.GetString(dbReader.GetOrdinal(COL_DIA_CHI));
                        rStaff.SDT = dbReader.GetString(dbReader.GetOrdinal(COL_SDT));
                        rStaff.MaNhom = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NHOM));
                        rStaff.Nhom = new GroupRepository(DbFactory).Find(rStaff.MaNhom);

                        _all.Add(rStaff);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<NhanVien>();
            }
        }

        public StaffRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public NhanVien Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findNhanVienById", new SqlParameter(PARAM_MA_NV, id));

                if (!dbReader.HasRows)
                    return new NhanVien();

                dbReader.Read();

                NhanVien rStaff = new NhanVien();
                rStaff.MaNV = id;
                rStaff.TenTK = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_TK));
                rStaff.MatKhau = dbReader.GetString(dbReader.GetOrdinal(COL_MAT_KHAU));
                rStaff.Email = dbReader.GetString(dbReader.GetOrdinal(COL_EMAIL));
                rStaff.TenNV = dbReader.GetString(dbReader.GetOrdinal(COL_TEN_NV));
                rStaff.CMND = dbReader.GetString(dbReader.GetOrdinal(COL_CMND));
                rStaff.DiaChi = dbReader.GetString(dbReader.GetOrdinal(COL_DIA_CHI));
                rStaff.SDT = dbReader.GetString(dbReader.GetOrdinal(COL_SDT));
                rStaff.MaNhom = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NHOM));
                rStaff.Nhom = new GroupRepository(DbFactory).Find(rStaff.MaNhom);

                return rStaff;
            }
            catch (Exception)
            {
            }

            return new NhanVien();
        }

        public int Save(NhanVien Staff)
        {
            return Save(Staff, false);
        }

        public int Save(NhanVien Staff, bool PasswordNotHash)
        {
            try
            {
                if (Staff.MaNV != 0)
                {
                    return DbContext.NonRecord("updateNhanVien",
                                    new SqlParameter(PARAM_MA_NV, Staff.MaNV),
                                    new SqlParameter(PARAM_TEN_TK, Staff.TenTK),
                                    new SqlParameter(PARAM_MAT_KHAU, PasswordNotHash == true ? Staff.MatKhau : Lib.Md5Sha1Encrypt.SHA1Hashing(Staff.MatKhau)),
                                    new SqlParameter(PARAM_EMAIL, Staff.Email),
                                    new SqlParameter(PARAM_TEN_NV, Staff.TenNV),
                                    new SqlParameter(PARAM_CMND, Staff.CMND),
                                    new SqlParameter(PARAM_DIA_CHI, Staff.DiaChi),
                                    new SqlParameter(PARAM_SDT, Staff.SDT),
                                    new SqlParameter(PARAM_MA_NHOM, Staff.MaNhom)
                                    );
                }
                //else
                return DbContext.NonRecord("insertNhanVien",
                                    new SqlParameter(PARAM_TEN_TK, Staff.TenTK),
                                    new SqlParameter(PARAM_MAT_KHAU, Lib.Md5Sha1Encrypt.SHA1Hashing(Staff.MatKhau)),
                                    new SqlParameter(PARAM_EMAIL, Staff.Email),
                                    new SqlParameter(PARAM_TEN_NV, Staff.TenNV),
                                    new SqlParameter(PARAM_CMND, Staff.CMND),
                                    new SqlParameter(PARAM_DIA_CHI, Staff.DiaChi),
                                    new SqlParameter(PARAM_SDT, Staff.SDT),
                                    new SqlParameter(PARAM_MA_NHOM, Staff.MaNhom)
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
                return DbContext.NonRecord("deleteNhanVien", new SqlParameter(PARAM_MA_NV, id));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public int Available(String Username, String Password, out String Role)
        {
            Role = String.Empty;

            try
            {
                DbDataReader dbReader = DbContext.RecordSets("loginNhanVien",
                                                    new SqlParameter(PARAM_TEN_TK, Username),
                                                    new SqlParameter(PARAM_MAT_KHAU, Lib.Md5Sha1Encrypt.SHA1Hashing(Password))
                                                    );

                if (!dbReader.HasRows)
                    return 0;

                dbReader.Read();

                int GroupId = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NHOM));
                Role = new GroupRepository(DbFactory).Find(GroupId).Role.Name;

                return dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NV));
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
                DbDataReader dbReader = DbContext.RecordSets("findNhanVienByTenTK", new SqlParameter(PARAM_TEN_TK, Username));

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
                DbDataReader dbReader = DbContext.RecordSets("findNhanVienByEmail", new SqlParameter(PARAM_EMAIL, Email));

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
                DbDataReader dbReader = DbContext.RecordSets("findNhanVienByCMND", new SqlParameter(PARAM_CMND, IdCardNumber));

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
                DbDataReader dbReader = DbContext.RecordSets("findNhanVienBySDT", new SqlParameter(PARAM_SDT, PhoneNum));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IEnumerable<KhachHang> GetCustomers(int StaffId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findAllKhachHangOfNhanVien", new SqlParameter(PARAM_MA_NV, StaffId));

                if (!dbReader.HasRows)
                    return new List<KhachHang>();

                KhachHang rCustomer;
                List<KhachHang> rsCustomers = new List<KhachHang>();

                while (dbReader.Read())
                {
                    rCustomer = new KhachHang();
                    rCustomer.MaKH = dbReader.GetInt32(dbReader.GetOrdinal(CustomerRepository.COL_MA_KH));
                    rCustomer.TenTK = dbReader.GetString(dbReader.GetOrdinal(CustomerRepository.COL_TEN_TK));
                    rCustomer.Email = dbReader.GetString(dbReader.GetOrdinal(CustomerRepository.COL_EMAIL));
                    rCustomer.TenKH = dbReader.GetString(dbReader.GetOrdinal(CustomerRepository.COL_TEN_KH));

                    rsCustomers.Add(rCustomer);
                }

                return rsCustomers;
            }
            catch (Exception)
            {
            }

            return new List<KhachHang>();
        }

        public IEnumerable<int> GetStaffIds()
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findAllMaNV");

                if (!dbReader.HasRows)
                    return new List<int>();

                List<int> staffIdSets = new List<int>();

                while (dbReader.Read())
                    staffIdSets.Add(dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NV)));

                return staffIdSets;
            }
            catch (Exception)
            {
            }

            return new List<int>();
        }

    }
}