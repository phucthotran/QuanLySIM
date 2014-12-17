USE [QuanLySIM]

GO

-- NHAN VIEN's TABLE

-- Dùng cho Login vào tài khoản nhân viên
CREATE PROCEDURE loginNhanVien @TenTK nvarchar(45), @MatKhau nvarchar(45)
AS
SELECT * FROM NhanVien
WHERE TenTK = @TenTK AND MatKhau = @MatKhau

GO

-- Tìm kiếm nhân viên thông qua khóa chính (MaNV)
CREATE PROCEDURE findNhanVienById @MaNV int
AS
SELECT * FROM NhanVien
WHERE MaNV = @MaNV

GO

-- Lấy ds tất cả nhân viên
CREATE PROCEDURE findAllNhanVien
AS
SELECT * FROM NhanVien

GO

-- Lấy ds tất cả khách hàng quản lý bởi 1 nhân viên
CREATE PROCEDURE findAllKhachHangOfNhanVien @MaNV int
AS
SELECT MaKH, TenTK, Email, TenKH
FROM KhachHang
WHERE MaNV = @MaNV

GO

-- Tìm kiếm nhân viên bằng tên tài khoản (Dùng cho việc kiểm tra sự tồn tại của TenTK trên hệ thống)
CREATE PROCEDURE findNhanVienByTenTK @TenTK nvarchar(45)
AS
SELECT * FROM NhanVien
WHERE TenTK = @TenTK

GO

-- Tìm kiếm nhân viên bằng Email (Dùng cho việc kiểm tra sự tồn tại của Email trên hệ thống)
CREATE PROCEDURE findNhanVienByEmail @Email nvarchar(45)
AS
SELECT * FROM NhanVien
WHERE Email = @Email

GO

-- Tìm nhân viên bằng CMND (Dùng cho việc kiểm tra sự tồn tại của CMND trên hệ thống)
CREATE PROCEDURE findNhanVienByCMND @CMND nvarchar(12)
AS
SELECT * FROM NhanVien
WHERE CMND = @CMND

GO

-- Tìm nhân viên bằng SĐT (Dùng cho việc kiểm tra sự tồn tại của SĐT trên hệ thống)
CREATE PROCEDURE findNhanVienBySDT @SDT nvarchar(13)
AS
SELECT * FROM NhanVien
WHERE SDT = @SDT

GO

-- Lấy tất cả MaNV của nhân viên để thực hiện Random nhân viên quản lý cho khách hàng
CREATE PROCEDURE findAllMaNV
AS
SELECT MaNV FROM NhanVien

GO

-- Đếm nhân viên
CREATE PROCEDURE countNhanVien
AS
SELECT COUNT(*) FROM NhanVien

GO

-- Insert nhân viên
CREATE PROCEDURE insertNhanVien @TenTK nvarchar(45), 
@MatKhau nvarchar(45), @Email nvarchar(45), @TenNV nvarchar(45), 
@CMND nvarchar(12), @DiaChi nvarchar(200), @SDT nvarchar(13), @MaNhom int
AS
INSERT INTO NhanVien(TenTK, MatKhau, Email, TenNV, CMND, DiaChi, SDT, MaNhom)
VALUES(@TenTK, @MatKhau, @Email, @TenNV, @CMND, @DiaChi, @SDT, @MaNhom)

GO

-- Xóa nhân viên
CREATE PROCEDURE deleteNhanVien @MaNV int
AS
DELETE FROM NhanVien
WHERE MaNV = @MaNV

GO

-- Update nhân viên
CREATE PROCEDURE updateNhanVien @MaNV int, @TenTK nvarchar(45), 
@MatKhau nvarchar(45), @Email nvarchar(45), @TenNV nvarchar(45), 
@CMND nvarchar(12), @DiaChi nvarchar(200), @SDT nvarchar(13), @MaNhom int
AS
UPDATE NhanVien
SET TenTK = @TenTK, MatKhau = @MatKhau, Email = @Email,
TenNV = @TenNV, CMND = @CMND, DiaChi = @DiaChi, SDT = @SDT, MaNhom = @MaNhom
WHERE MaNV = @MaNV

GO

-- NHOM's TABLE

-- Tìm nhóm thông qua khóa chính (MaNhom)
CREATE PROCEDURE findNhomById @MaNhom int
AS
SELECT * FROM Nhom
WHERE MaNhom = @MaNhom

GO

-- Lấy ds tất cả các nhóm
CREATE PROCEDURE findAllNhom
AS
SELECT * FROM Nhom

GO

-- Lấy ds tất cả nhân viên trong 1 nhóm
CREATE PROCEDURE findAllNhanVienOfNhom @MaNhom int
AS
SELECT MaNV, TenTK, Email, TenNV
FROM NhanVien
WHERE MaNhom = @MaNhom

GO

-- Tìm nhóm theo tên (Dùng cho việc kiểm tra sự tồn tại của Tên nhóm)
CREATE PROCEDURE findNhomByTen @Ten nvarchar(20)
AS
SELECT * FROM Nhom
WHERE Ten = @Ten

GO

-- Đếm nhóm
CREATE PROCEDURE countNhom
AS
SELECT COUNT(*) FROM Nhom

GO

-- Insert nhóm
CREATE PROCEDURE insertNhom @Ten nvarchar(20), @MoTa nvarchar(100), @RoleId int
AS
INSERT INTO Nhom(Ten, MoTa, RoleId)
VALUES(@Ten, @MoTa, @RoleId)

GO

-- Xóa nhóm
CREATE PROCEDURE deleteNhom @MaNhom int
AS
DELETE FROM Nhom
WHERE MaNhom = @MaNhom

GO

-- Update nhóm
CREATE PROCEDURE updateNhom @MaNhom int, @Ten nvarchar(20), @MoTa nvarchar(100), @RoleId int
AS
UPDATE Nhom
SET Ten = @Ten, MoTa = @MoTa, RoleId = @RoleId
WHERE MaNhom = @MaNhom

GO

-- KHACH HANG's TABLE

-- Dùng cho Login vào tài khoản của Khách hàng
CREATE PROCEDURE loginKhachHang @TenTK nvarchar(45), @MatKhau nvarchar(45)
AS
SELECT * FROM KhachHang
WHERE TenTK = @TenTK AND MatKhau = @MatKhau

GO

-- Tìm khách hàng thông qua TenTK (Dùng cho việc kiểm tra sự tồn tại của TenTK trên hệ thống)
CREATE PROCEDURE findKhachHangByTenTK @TenTK nvarchar(45)
AS
SELECT * FROM KhachHang
WHERE TenTK = @TenTK

GO

-- Tìm khách hàng thông qua Email (Dùng cho việc kiểm tra sự tồn tại của Email trên hệ thống)
CREATE PROCEDURE findKhachHangByEmail @Email nvarchar(45)
AS
SELECT * FROM KhachHang
WHERE Email = @Email

GO

-- Tìm khách hàng thông qua CMND (Dùng cho việc kiểm tra sự tồn tại của CMND trên hệ thống)
CREATE PROCEDURE findKhachHangByCMND @CMND nvarchar(12)
AS
SELECT * FROM KhachHang
WHERE CMND = @CMND

GO

-- Tìm khách hàng thông qua SDT (Dùng cho việc kiểm tra sự tồn tại của SDT trên hệ thống)
CREATE PROCEDURE findKhachHangBySDT @SDT nvarchar(13)
AS
SELECT * FROM KhachHang
WHERE SDT = @SDT

GO

-- Lấy ra MaKH của bản ghi được tạo gần đây nhất
CREATE PROCEDURE findLastKhachHangMaKH
AS
SELECT MAX(MaKH) as 'LastMaKH' FROM KhachHang

GO

-- Lấy ds tất cả các Khách hàng
CREATE PROCEDURE findAllKhachHang
AS
SELECT * FROM KhachHang

GO

-- Tìm khách hàng thông qua khóa chính (MaKH)
CREATE PROCEDURE findKhachHangById @MaKH int
AS
SELECT * FROM KhachHang
WHERE MaKH = @MaKH

GO

-- Đếm khách hàng
CREATE PROCEDURE countKhachHang
AS
SELECT COUNT(*) FROM KhachHang

GO

-- Insert khách hàng (đầy đủ)
CREATE PROCEDURE insertKhachHang @TenTK nvarchar(45), @MatKhau nvarchar(45), @Email nvarchar(45), 
@TenKH nvarchar(45), @CMND nvarchar(12), @DiaChi nvarchar(200), @SDT nvarchar(13), @MaNV int
AS
INSERT INTO KhachHang(TenTK, MatKhau, Email, TenKH, CMND, DiaChi, SoLuongDaMua, SDT, MaNV)
VALUES(@TenTK, @MatKhau, @Email, @TenKH, @CMND, @DiaChi, 0, @SDT, @MaNV)

GO

-- Insert khách hàng (bản rút gọn, dùng để insert khách hàng mới khi khách hàng thực hiện đăng ký tài khoản mới)
CREATE PROCEDURE insertLittleKhachHang @TenTK nvarchar(45), @MatKhau nvarchar(45), @Email nvarchar(45), @MaNV int
AS
INSERT INTO KhachHang (TenTK, MatKhau, Email, TenKH, CMND, DiaChi, SDT, SoLuongDaMua, MaNV)
VALUES (@TenTK, @MatKhau, @Email, '', '', '', '', 0, @MaNV)

GO

-- Xóa khách hàng
CREATE PROCEDURE deleteKhachHang @MaKH int
AS
DELETE FROM KhachHang
WHERE MaKH = @MaKH

GO

-- Update khách hàng
CREATE PROCEDURE updateKhachHang @MaKH int, @TenTK nvarchar(45), @MatKhau nvarchar(45), @Email nvarchar(45),
@TenKH nvarchar(45), @CMND nvarchar(12), @DiaChi nvarchar(200), @SDT nvarchar(13), @MaNV int
AS
UPDATE KhachHang
SET TenTK = @TenTK, MatKhau = @MatKhau, Email = @Email, 
TenKH = @TenKH, CMND = @CMND, DiaChi = @DiaChi, SDT = @SDT, MaNV = @MaNV
WHERE MaKH = @MaKH

GO

-- Update số lượng đơn hàng đã đặt
CREATE PROCEDURE updateKhachHangOrderAmount @MaKH int, @SoLuongMua int
AS
UPDATE KhachHang
SET SoLuongDaMua = @SoLuongMua
WHERE MaKH = @MaKH

GO

-- SIM's TABLE

-- Tìm SIM thông qua khóa chính (SimId)
CREATE PROCEDURE findSIMById @SimId int
AS
SELECT * FROM SIM
WHERE SimId = @SimId

GO

-- Lấy ds tất cả các SIM
CREATE PROCEDURE findAllSIM
AS
SELECT * FROM SIM

GO

-- Lấy ds tất cả các SIM mới nhất (hiển thị ở trang chủ)
CREATE PROCEDURE findNewestSIM
AS
SELECT TOP(10) * FROM SIM

GO

-- Tìm SIM thông qua Mã serial SIM (Dùng để kiểm tra sự tồn tại của Mã SIM trên hệ thống)
CREATE PROCEDURE findSIMByMaSIM @MaSIM nvarchar(16)
AS
SELECT * FROM SIM
WHERE MaSIM = @MaSIM

GO

-- Tìm SIM thông qua SoThueBao (Dùng để kiểm tra sự tồn tại của SoThueBao trên hệ thống)
CREATE PROCEDURE findSIMBySoThueBao @SoThueBao nvarchar(13)
AS
SELECT * FROM SIM
WHERE SoThueBao = @SoThueBao

GO

-- Đếm SIM
CREATE PROCEDURE countSIM
AS
SELECT COUNT(*) FROM SIM

GO

-- Insert SIM
CREATE PROCEDURE insertSIM @MaSIM nvarchar(16), @SoThueBao nvarchar(13), @GiaTien decimal, @TinhTrang nvarchar(20)
AS
INSERT INTO SIM(MaSIM, SoThueBao, GiaTien, TinhTrang)
VALUES(@MaSIM, @SoThueBao, @GiaTien, @TinhTrang)

GO

-- Xóa SIM
CREATE PROCEDURE deleteSIM @SimId int
AS
DELETE FROM SIM
WHERE SimId = @SimId

GO

-- Xóa SIM thông qua Mã serial SIM
CREATE PROCEDURE deleteSIMByMaSIM @MaSIM nvarchar(20)
AS
DELETE FROM SIM
WHERE MaSIM = @MaSIM

GO

-- Update SIM
CREATE PROCEDURE updateSIM @SimId int, @GiaTien decimal, @TinhTrang nvarchar(20)
AS
UPDATE SIM
SET GiaTien = @GiaTien, TinhTrang = @TinhTrang
WHERE SimId = @SimId

GO

-- PHIEU MUA's TABLE

-- Tìm phiếu mua thông qua khóa chính (MaPM)
CREATE PROCEDURE findPhieuMuaById @MaPM int
AS
SELECT * FROM PhieuMua
WHERE MaPM = @MaPM

GO

-- Lấy ds tất cả các phiếu mua
CREATE PROCEDURE findAllPhieuMua
AS
SELECT * FROM PhieuMua

GO

-- Lấy ds tất cả các phiếu mua trong 7 ngày
CREATE PROCEDURE findAllPhieuMuaOrderInWeek
AS
SELECT TOP(10) pm.MaPM, pm.NgayDatMua, pm.NgayGiao, pm.TongTien, pm.SimId, si.SoThueBao, pm.MaKH
FROM PhieuMua as pm
LEFT JOIN SIM as si
ON pm.SimId = si.SimId
WHERE DATEDIFF(day, pm.NgayDatMua, GETDATE()) <= 7

GO

-- Tìm phiếu mua thông qua một SIM bất kì (Dựa vào khóa SimId)
CREATE PROCEDURE findPhieuMuaBySimId @SimId int
AS
SELECT * FROM PhieuMua
WHERE SimId = @SimId

GO

-- Tìm kiếm phiếu mua bằng MaKH và SimId (Dùng cho việc xác định 2 phiếu mua có trùng khớp hay không,..)
CREATE PROCEDURE findPhieuMuaByMaKHandSimId @MaKH int, @SimId int
AS
SELECT * FROM PhieuMua
WHERE MaKH = @MaKH AND SimId = @SimId

GO

-- Lấy ds phiếu mua của một khách hàng thông qua MaKH và TinhTrang của SIM (Đã bán, chưa thanh toán,..)
CREATE PROCEDURE findPhieuMuaOrderdByMaKH @MaKH int, @TinhTrang nvarchar(20)
AS
IF(LEN(@TinhTrang) > 0)
BEGIN
	SELECT pm.MaPM, pm.NgayDatMua, pm.NgayGiao, pm.TongTien, pm.SimId, pm.MaKH
	FROM PhieuMua as pm
	LEFT JOIN SIM as si
	ON pm.SimId = si.SimId
	WHERE pm.MaKH = @MaKH AND si.TinhTrang = @TinhTrang
END
ELSE
BEGIN
	SELECT pm.MaPM, pm.NgayDatMua, pm.NgayGiao, pm.TongTien, pm.SimId, pm.MaKH
	FROM PhieuMua as pm
	LEFT JOIN SIM as si
	ON pm.SimId = si.SimId
	WHERE pm.MaKH = @MaKH
END

GO

-- Đếm phiếu mua
CREATE PROCEDURE countPhieuMua
AS
SELECT COUNT(*) FROM PhieuMua

GO

-- Insert phiếu mua (bản rút gọn) và tính tổng tiền cần thanh toán
CREATE PROCEDURE insertPhieuMuaLittle @MaKH int, @SimId int
AS
DECLARE @GiaTien decimal
DECLARE @TongTien decimal
SET @GiaTien = (SELECT GiaTien FROM SIM WHERE SimId = @SimId)
SET @TongTien = @GiaTien + ( (@GiaTien * 5) / 100 ) -- TongTien = GiaTien + (GiaTien * 5%)
INSERT INTO PhieuMua(NgayDatMua, NgayGiao, TongTien,  MaKH, SimId)
VALUES(GETDATE(), GETDATE() + 3, @TongTien, @MaKH, @SimId)

GO

-- Insert phiếu mua
CREATE PROCEDURE insertPhieuMua @MaKH int, @NgayDatMua datetime, @NgayGiao datetime, @TongTien decimal, @SimId int
AS
INSERT INTO PhieuMua(NgayDatMua, NgayGiao, TongTien, MaKH, SimId)
VALUES(@NgayDatMua, @NgayGiao, @TongTien, @MaKH, @SimId)

GO

-- Xóa phiếu mua
CREATE PROCEDURE deletePhieuMua @MaPM int
AS
DELETE FROM PhieuMua
WHERE MaPM = @MaPM

GO

-- Xóa phiếu mua thông qua MaKH
CREATE PROCEDURE deletePhieuMuaByMaKH @MaKH int
AS
DELETE FROM PhieuMua
WHERE MaKH = @MaKH

GO

-- Update phiếu mua
CREATE PROCEDURE updatePhieuMua @MaPM int, @NgayDatMua datetime, @NgayGiao datetime, @TongTien decimal, @SimId int
AS
UPDATE PhieuMua
SET NgayDatMua = @NgayDatMua, NgayGiao = @NgayGiao, TongTien = @TongTien, SimId = @SimId
WHERE MaPM = @MaPM

GO

-- ROLE's TABLE

-- Tìm Role thông qua khóa chính (RoleId)
CREATE PROCEDURE findRoleById @RoleId int
AS
SELECT * FROM Role
WHERE RoleId = @RoleId

GO

-- Tìm tất cả các Role
CREATE PROCEDURE findAllRole
AS
SELECT * FROM Role

GO