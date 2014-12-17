namespace QuanLySIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        MaNV = c.Int(nullable: false, identity: true),
                        TenTK = c.String(nullable: false, maxLength: 45),
                        MatKhau = c.String(nullable: false, maxLength: 45),
                        Email = c.String(nullable: false, maxLength: 45),
                        TenNV = c.String(nullable: false, maxLength: 45),
                        CMND = c.String(nullable: false, maxLength: 12),
                        DiaChi = c.String(nullable: false, maxLength: 200),
                        SDT = c.String(nullable: false, maxLength: 13),
                        MaNhom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaNV)
                .ForeignKey("dbo.Nhom", t => t.MaNhom, cascadeDelete: true)
                .Index(t => t.MaNhom);
            
            CreateTable(
                "dbo.Nhom",
                c => new
                    {
                        MaNhom = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 20),
                        MoTa = c.String(nullable: false, maxLength: 100),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaNhom)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        MaKH = c.Int(nullable: false, identity: true),
                        TenTK = c.String(nullable: false, maxLength: 45),
                        MatKhau = c.String(nullable: false, maxLength: 45),
                        Email = c.String(nullable: false, maxLength: 45),
                        TenKH = c.String(nullable: false, maxLength: 45),
                        CMND = c.String(nullable: false, maxLength: 12),
                        DiaChi = c.String(nullable: false, maxLength: 200),
                        SDT = c.String(maxLength: 13),
                        SoLuongDaMua = c.Int(nullable: false),
                        MaNV = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaKH)
                .ForeignKey("dbo.NhanVien", t => t.MaNV, cascadeDelete: true)
                .Index(t => t.MaNV);
            
            CreateTable(
                "dbo.PhieuMua",
                c => new
                    {
                        MaPM = c.Int(nullable: false, identity: true),
                        NgayDatMua = c.DateTime(nullable: false),
                        NgayGiao = c.DateTime(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaKH = c.Int(nullable: false),
                        SimId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaPM)
                .ForeignKey("dbo.KhachHang", t => t.MaKH, cascadeDelete: true)
                .ForeignKey("dbo.SIM", t => t.SimId, cascadeDelete: true)
                .Index(t => t.MaKH)
                .Index(t => t.SimId);
            
            CreateTable(
                "dbo.SIM",
                c => new
                    {
                        SimId = c.Int(nullable: false, identity: true),
                        MaSIM = c.String(nullable: false, maxLength: 16),
                        SoThueBao = c.String(nullable: false, maxLength: 13),
                        GiaTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TinhTrang = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SimId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PhieuMua", new[] { "SimId" });
            DropIndex("dbo.PhieuMua", new[] { "MaKH" });
            DropIndex("dbo.KhachHang", new[] { "MaNV" });
            DropIndex("dbo.Nhom", new[] { "RoleId" });
            DropIndex("dbo.NhanVien", new[] { "MaNhom" });
            DropForeignKey("dbo.PhieuMua", "SimId", "dbo.SIM");
            DropForeignKey("dbo.PhieuMua", "MaKH", "dbo.KhachHang");
            DropForeignKey("dbo.KhachHang", "MaNV", "dbo.NhanVien");
            DropForeignKey("dbo.Nhom", "RoleId", "dbo.Role");
            DropForeignKey("dbo.NhanVien", "MaNhom", "dbo.Nhom");
            DropTable("dbo.SIM");
            DropTable("dbo.PhieuMua");
            DropTable("dbo.KhachHang");
            DropTable("dbo.Role");
            DropTable("dbo.Nhom");
            DropTable("dbo.NhanVien");
        }
    }
}
