namespace QuanLySIM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using QuanLySIM.Entities;    

    internal sealed class Configuration : DbMigrationsConfiguration<QuanLySIM.Data.QuanLySIMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuanLySIM.Data.QuanLySIMContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Role.AddOrUpdate(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "Staff" }
            );

            context.Group.AddOrUpdate(
                new Nhom { MaNhom = 1, Ten = "Quản Trị", MoTa = "Nhóm quản lý cấp cao", RoleId = 1 },
                new Nhom { MaNhom = 2, Ten = "Nhân Viên", MoTa = "Nhóm nhân viên quản lý thông Tin SIM, Khách Hàng,..", RoleId = 2 }
            );

            context.Staff.AddOrUpdate(
                new NhanVien { MaNV = 1, TenTK = "admin123", MatKhau = Lib.Md5Sha1Encrypt.SHA1Hashing("admin123"), Email = "admin@quanlysim.com", TenNV = "Administrator", DiaChi = "ĐH Khoa Học Tự Nhiên", CMND = "123456789", SDT = "123456789", MaNhom = 1  }
            );
        }
    }
}
