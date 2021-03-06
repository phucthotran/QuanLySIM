USE [QuanLySIM]
GO
/****** Object:  Table [dbo].[SIM]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIM](
	[SimId] [int] IDENTITY(1,1) NOT NULL,
	[MaSIM] [nvarchar](16) NOT NULL,
	[SoThueBao] [nvarchar](13) NOT NULL,
	[GiaTien] [decimal](18, 2) NOT NULL,
	[TinhTrang] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.SIM] PRIMARY KEY CLUSTERED 
(
	[SimId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhom]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhom](
	[MaNhom] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[MoTa] [nvarchar](100) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Nhom] PRIMARY KEY CLUSTERED 
(
	[MaNhom] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[TenTK] [nvarchar](45) NOT NULL,
	[MatKhau] [nvarchar](45) NOT NULL,
	[Email] [nvarchar](45) NOT NULL,
	[TenNV] [nvarchar](45) NOT NULL,
	[CMND] [nvarchar](12) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[SDT] [nvarchar](13) NOT NULL,
	[MaNhom] [int] NOT NULL,
 CONSTRAINT [PK_dbo.NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenTK] [nvarchar](45) NOT NULL,
	[MatKhau] [nvarchar](45) NOT NULL,
	[Email] [nvarchar](45) NOT NULL,
	[TenKH] [nvarchar](45) NOT NULL,
	[CMND] [nvarchar](12) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[SDT] [nvarchar](13) NULL,
	[SoLuongDaMua] [int] NOT NULL,
	[MaNV] [int] NOT NULL,
 CONSTRAINT [PK_dbo.KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuMua]    Script Date: 10/24/2013 09:39:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuMua](
	[MaPM] [int] IDENTITY(1,1) NOT NULL,
	[NgayDatMua] [datetime] NOT NULL,
	[NgayGiao] [datetime] NOT NULL,
	[TongTien] [decimal](18, 2) NOT NULL,
	[MaKH] [int] NOT NULL,
	[SimId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PhieuMua] PRIMARY KEY CLUSTERED 
(
	[MaPM] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_dbo.KhachHang_dbo.NhanVien_MaNV]    Script Date: 10/24/2013 09:39:30 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KhachHang_dbo.NhanVien_MaNV] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_dbo.KhachHang_dbo.NhanVien_MaNV]
GO
/****** Object:  ForeignKey [FK_dbo.NhanVien_dbo.Nhom_MaNhom]    Script Date: 10/24/2013 09:39:30 ******/
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_dbo.NhanVien_dbo.Nhom_MaNhom] FOREIGN KEY([MaNhom])
REFERENCES [dbo].[Nhom] ([MaNhom])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_dbo.NhanVien_dbo.Nhom_MaNhom]
GO
/****** Object:  ForeignKey [FK_dbo.Nhom_dbo.Role_RoleId]    Script Date: 10/24/2013 09:39:30 ******/
ALTER TABLE [dbo].[Nhom]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Nhom_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Nhom] CHECK CONSTRAINT [FK_dbo.Nhom_dbo.Role_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.PhieuMua_dbo.KhachHang_MaKH]    Script Date: 10/24/2013 09:39:30 ******/
ALTER TABLE [dbo].[PhieuMua]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PhieuMua_dbo.KhachHang_MaKH] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuMua] CHECK CONSTRAINT [FK_dbo.PhieuMua_dbo.KhachHang_MaKH]
GO
/****** Object:  ForeignKey [FK_dbo.PhieuMua_dbo.SIM_SimId]    Script Date: 10/24/2013 09:39:30 ******/
ALTER TABLE [dbo].[PhieuMua]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PhieuMua_dbo.SIM_SimId] FOREIGN KEY([SimId])
REFERENCES [dbo].[SIM] ([SimId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuMua] CHECK CONSTRAINT [FK_dbo.PhieuMua_dbo.SIM_SimId]
GO
