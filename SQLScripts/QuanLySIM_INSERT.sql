USE [QuanLySIM]
GO
/****** Object:  Table [dbo].[SIM]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[SIM] ON
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (1, N'3858590784363584', N'5646548543', CAST(600.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (2, N'3858590784363585', N'5646548544', CAST(200.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (3, N'3858590784363586', N'5646548545', CAST(800.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (4, N'3858590784363587', N'5646548546', CAST(700.00 AS Decimal(18, 2)), N'Not Paid')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (5, N'3858590784363588', N'5646548547', CAST(900.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (6, N'3858590784363589', N'5646548548', CAST(800.00 AS Decimal(18, 2)), N'Not Paid')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (7, N'3858590784363590', N'5646548549', CAST(450.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (8, N'3858590784363591', N'5646548550', CAST(350.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (9, N'3858590784363592', N'5646548551', CAST(200.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (10, N'3858590784363593', N'5646548552', CAST(500.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (11, N'3858590784363594', N'5646548553', CAST(600.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (12, N'3858590784363595', N'5646548554', CAST(900.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (13, N'3858590784363596', N'5646548555', CAST(500.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (14, N'3858590784363597', N'5646548556', CAST(200.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[SIM] ([SimId], [MaSIM], [SoThueBao], [GiaTien], [TinhTrang]) VALUES (15, N'3858590784363598', N'5646548557', CAST(900.00 AS Decimal(18, 2)), N'Available')
SET IDENTITY_INSERT [dbo].[SIM] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([RoleId], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleId], [Name]) VALUES (2, N'Staff')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Nhom]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[Nhom] ON
INSERT [dbo].[Nhom] ([MaNhom], [Ten], [MoTa], [RoleId]) VALUES (1, N'Quản Trị', N'Nhóm quản lý cấp cao', 1)
INSERT [dbo].[Nhom] ([MaNhom], [Ten], [MoTa], [RoleId]) VALUES (2, N'Nhân Viên', N'Nhóm nhân viên quản lý thông Tin SIM, Khách Hàng,..', 2)
SET IDENTITY_INSERT [dbo].[Nhom] OFF
/****** Object:  Table [dbo].[NhanVien]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[NhanVien] ON
INSERT [dbo].[NhanVien] ([MaNV], [TenTK], [MatKhau], [Email], [TenNV], [CMND], [DiaChi], [SDT], [MaNhom]) VALUES (1, N'admin123', N'f865b53623b121fd34ee5426c792e5c33af8c227', N'admin@quanlysim.com', N'Administrator', N'123456789', N'ĐH Khoa Học Tự Nhiên', N'123456789', 1)
INSERT [dbo].[NhanVien] ([MaNV], [TenTK], [MatKhau], [Email], [TenNV], [CMND], [DiaChi], [SDT], [MaNhom]) VALUES (2, N'itexplore', N'7c222fb2927d828af22f592134e8932480637c0d', N'itexplore09@yahoo.com.vn', N'Trần Phúc Thọ', N'245219205', N'1758B Phạm Thế Hiển, P6, Q8, HCM', N'01655973646', 1)
INSERT [dbo].[NhanVien] ([MaNV], [TenTK], [MatKhau], [Email], [TenNV], [CMND], [DiaChi], [SDT], [MaNhom]) VALUES (3, N'shinichi', N'7c222fb2927d828af22f592134e8932480637c0d', N'shinichi@gmail.com', N'Nguyễn Hoàng Hà', N'245219206', N'Hồng Bàng, Q5', N'0385490556541', 2)
INSERT [dbo].[NhanVien] ([MaNV], [TenTK], [MatKhau], [Email], [TenNV], [CMND], [DiaChi], [SDT], [MaNhom]) VALUES (4, N'nhanvien1', N'7c222fb2927d828af22f592134e8932480637c0d', N'nhanvien1@gmail.com', N'Trần Văn E', N'34589459446', N'Q.Tân Phú, HCM', N'038549055654', 2)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[KhachHang] ON
INSERT [dbo].[KhachHang] ([MaKH], [TenTK], [MatKhau], [Email], [TenKH], [CMND], [DiaChi], [SDT], [SoLuongDaMua], [MaNV]) VALUES (2, N'itexplore', N'7c222fb2927d828af22f592134e8932480637c0d', N'itexplore09@yahoo.com.vn', N'Trần Bình Trọng', N'2452192051', N'Hồng Bàng, Q5', N'038549055654', 0, 3)
INSERT [dbo].[KhachHang] ([MaKH], [TenTK], [MatKhau], [Email], [TenKH], [CMND], [DiaChi], [SDT], [SoLuongDaMua], [MaNV]) VALUES (3, N'itexplore09', N'7c222fb2927d828af22f592134e8932480637c0d', N'itexplore09@gmail.com', N'Trần Bình Trọng E', N'3243654659', N'Hồng Bàng, Q5', N'0385490556541', 0, 2)
INSERT [dbo].[KhachHang] ([MaKH], [TenTK], [MatKhau], [Email], [TenKH], [CMND], [DiaChi], [SDT], [SoLuongDaMua], [MaNV]) VALUES (6, N'itexplore08', N'7c222fb2927d828af22f592134e8932480637c0d', N'itexplore08@gmail.com', N'Nguyễn Phương Hà', N'34589459446', N'Q.Tân Phú, HCM', N'30985860956', 0, 3)
INSERT [dbo].[KhachHang] ([MaKH], [TenTK], [MatKhau], [Email], [TenKH], [CMND], [DiaChi], [SDT], [SoLuongDaMua], [MaNV]) VALUES (8, N'sunnylove', N'7c222fb2927d828af22f592134e8932480637c0d', N'shinichi@gmail.com', N'Nguyễn Thanh Trang', N'245219206', N'Phan Văn Hớn, Q12', N'0385490534534', 0, 2)
INSERT [dbo].[KhachHang] ([MaKH], [TenTK], [MatKhau], [Email], [TenKH], [CMND], [DiaChi], [SDT], [SoLuongDaMua], [MaNV]) VALUES (10, N'shinbimbim', N'7c222fb2927d828af22f592134e8932480637c0d', N'shinbimbim@gmail.com', N'', N'', N'', N'', 0, 2)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
/****** Object:  Table [dbo].[PhieuMua]    Script Date: 10/24/2013 09:41:01 ******/
SET IDENTITY_INSERT [dbo].[PhieuMua] ON
INSERT [dbo].[PhieuMua] ([MaPM], [NgayDatMua], [NgayGiao], [TongTien], [MaKH], [SimId]) VALUES (35, CAST(0x0000A25F0170E06A AS DateTime), CAST(0x0000A2640170E06A AS DateTime), CAST(735.00 AS Decimal(18, 2)), 3, 4)
INSERT [dbo].[PhieuMua] ([MaPM], [NgayDatMua], [NgayGiao], [TongTien], [MaKH], [SimId]) VALUES (36, CAST(0x0000A25F00000000 AS DateTime), CAST(0x0000A26200000000 AS DateTime), CAST(1000.00 AS Decimal(18, 2)), 8, 6)
SET IDENTITY_INSERT [dbo].[PhieuMua] OFF
