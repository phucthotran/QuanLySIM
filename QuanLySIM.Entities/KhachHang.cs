using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [ScaffoldColumn(false)]
        public int MaKH { get; set; }

        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống")]
        [MinLength(6, ErrorMessage = "Tên tài khoản tối thiểu 16 kí tự")]
        [MaxLength(45, ErrorMessage = "Tên tài khoản tối đa 45 kí tự")]
        public String TenTK { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu tối thiểu 8 kí tự")]
        [MaxLength(45, ErrorMessage = "Mật khẩu tối đa 45 kí tự")]
        public String MatKhau { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không hợp lệ")]
        [MinLength(6, ErrorMessage = "Email tối thiểu 6 kí tự")]
        [MaxLength(45, ErrorMessage = "Email tối đa 45 kí tự")]
        public String Email { get; set; }

        [Display(Name = "Tên khách hàng", Description = "Tên họ thật dùng trong giao dịch")]
        [Required(ErrorMessage = "Tên khách hàng không được bỏ trống")]
        [MinLength(3, ErrorMessage = "Tên khách hàng tối thiểu 3 kí tự")]
        [MaxLength(45, ErrorMessage = "Tên khách hàng không quá 45 kí tự")]
        public String TenKH { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "CMND không được bỏ trống")]
        [MinLength(9, ErrorMessage = "CMND tối thiểu 9 kí tự")]
        [MaxLength(12, ErrorMessage = "CMND tối đa 12 kí tự")]
        public String CMND { get; set; }

        [Display(Name = "Địa chỉ", Description = "Địa chỉ dùng khi giao hàng")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [MinLength(10, ErrorMessage = "Địa chỉ tối thiểu 10 kí tự")]
        [MaxLength(200, ErrorMessage = "Địa chỉ tối đa 200 kí tự")]
        public String DiaChi { get; set; }

        [Display(Name = "SĐT", Description = "SĐT dùng để liên lạc khi giao hàng")]
        [MinLength(9, ErrorMessage = "SĐT tối thiểu 9 kí tự")]
        [MaxLength(13, ErrorMessage = "SĐT tối đa 13 kí tự")]
        public String SDT { get; set; }

        [Display(Name = "Số lượng đã đặt mua")]
        public int SoLuongDaMua { get; set; }

        [NotMapped]
        [Display(Name = "Số lượng đang đặt mua")]
        public int SoLuongDangDatMua { get; set; }

        public int MaNV { get; set; }

        [ForeignKey("MaNV")]
        public NhanVien NhanVien { get; set; }

        [NotMapped]
        public IList<PhieuMua> PhieuMua { get; set; }

        public bool IsNotCompleted()
        {
            return String.IsNullOrEmpty(this.TenKH) || String.IsNullOrEmpty(this.CMND) || String.IsNullOrEmpty(this.DiaChi) || String.IsNullOrEmpty(this.SDT);
        }

        public override bool Equals(object obj)
        {
            if (obj is KhachHang)
            {
                KhachHang compareObj = (KhachHang)obj;

                if (compareObj.MaKH == MaKH)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}