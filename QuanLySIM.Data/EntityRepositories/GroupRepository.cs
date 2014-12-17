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
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public const String COL_MA_NHOM = "MaNhom";
        public const String COL_TEN = "Ten";
        public const String COL_MO_TA = "MoTa";
        public const String COL_ROLE_ID = "RoleId";

        public const String PARAM_MA_NHOM = "MaNhom";
        public const String PARAM_TEN = "Ten";
        public const String PARAM_MO_TA = "MoTa";
        public const String PARAM_ROLE_ID = "RoleId";

        private int _count = 0;
        private List<Nhom> _all = null;

        public int Count
        {
            get
            {
                try
                {
                    _count = (int)DbContext.ScalarRecord("countNhom");
                    return _count;
                }
                catch (Exception)
                {
                }

                return 0;
            }
        }

        public IEnumerable<Nhom> All
        {
            get
            {
                try
                {
                    DbDataReader dbReader = DbContext.RecordSets("findAllNhom");

                    if (!dbReader.HasRows)
                        return new List<Nhom>();

                    Nhom rGroup;
                    _all = new List<Nhom>();

                    while (dbReader.Read())
                    {
                        rGroup = new Nhom();
                        rGroup.MaNhom = dbReader.GetInt32(dbReader.GetOrdinal(COL_MA_NHOM));
                        rGroup.Ten = dbReader.GetString(dbReader.GetOrdinal(COL_TEN));
                        rGroup.MoTa = dbReader.GetString(dbReader.GetOrdinal(COL_MO_TA));
                        rGroup.RoleId = dbReader.GetInt32(dbReader.GetOrdinal(COL_ROLE_ID));
                        rGroup.Role = new RoleRepository(DbFactory).Find(rGroup.RoleId);

                        _all.Add(rGroup);
                    }

                    return _all;
                }
                catch (Exception)
                {
                }

                return new List<Nhom>();
            }
        }

        public GroupRepository(IDbFactory DbFactory)
            : base(DbFactory)
        {
        }

        public Nhom Find(int id)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findNhomById", new SqlParameter(PARAM_MA_NHOM, id));

                if (!dbReader.HasRows)
                    return new Nhom();

                dbReader.Read();

                Nhom rGroup = new Nhom();
                rGroup.MaNhom = id;
                rGroup.Ten = dbReader.GetString(dbReader.GetOrdinal(COL_TEN));
                rGroup.MoTa = dbReader.GetString(dbReader.GetOrdinal(COL_MO_TA));
                rGroup.RoleId = dbReader.GetInt32(dbReader.GetOrdinal(COL_ROLE_ID));
                rGroup.Role = new RoleRepository(DbFactory).Find(rGroup.RoleId);

                return rGroup;
            }
            catch (Exception)
            {
            }

            return new Nhom();
        }

        public int Save(Nhom Group)
        {
            try
            {
                if (Group.MaNhom != 0)
                {
                    return DbContext.NonRecord("updateNhom",
                                    new SqlParameter(PARAM_MA_NHOM, Group.MaNhom),
                                    new SqlParameter(PARAM_TEN, Group.Ten),
                                    new SqlParameter(PARAM_MO_TA, Group.MoTa),
                                    new SqlParameter(PARAM_ROLE_ID, Group.RoleId)
                                    );
                }
                //else
                return DbContext.NonRecord("insertNhom",
                                    new SqlParameter(PARAM_TEN, Group.Ten),
                                    new SqlParameter(PARAM_MO_TA, Group.MoTa),
                                    new SqlParameter(PARAM_ROLE_ID, Group.RoleId)
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
                return DbContext.NonRecord("deleteNhom", new SqlParameter(PARAM_MA_NHOM, id));
            }
            catch (Exception)
            {
            }

            return -1;
        }

        public bool IsExist(String Name)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findNhomByTen", new SqlParameter(PARAM_TEN, Name));

                if (dbReader.HasRows)
                    return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public IEnumerable<NhanVien> GetStaffs(int GroupId)
        {
            try
            {
                DbDataReader dbReader = DbContext.RecordSets("findAllNhanVienOfNhom", new SqlParameter(PARAM_MA_NHOM, GroupId));

                if (!dbReader.HasRows)
                    return new List<NhanVien>();

                NhanVien rStaff;
                List<NhanVien> rsStaffs = new List<NhanVien>();

                while (dbReader.Read())
                {
                    rStaff = new NhanVien();
                    rStaff.MaNV = dbReader.GetInt32(dbReader.GetOrdinal(StaffRepository.COL_MA_NV));
                    rStaff.TenTK = dbReader.GetString(dbReader.GetOrdinal(StaffRepository.COL_TEN_TK));
                    rStaff.Email = dbReader.GetString(dbReader.GetOrdinal(StaffRepository.COL_EMAIL));
                    rStaff.TenNV = dbReader.GetString(dbReader.GetOrdinal(StaffRepository.COL_TEN_NV));

                    rsStaffs.Add(rStaff);
                }

                return rsStaffs;
            }
            catch (Exception)
            {
            }

            return new List<NhanVien>();
        }
    }
}