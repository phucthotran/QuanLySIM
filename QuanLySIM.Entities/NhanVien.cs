using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [ScaffoldColumn(false)]
        public int MaNV { get; set; }

        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống")]
        [MinLength(6, ErrorMessage = "Tên tài khoản tối thiểu 6 kí tự")]
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

        [Display(Name = "Tên nhân viên")]
        [Required(ErrorMessage = "Tên nhân viên không được bỏ trống")]
        [MinLength(3, ErrorMessage = "Tên nhân viên tối thiểu 3 kí tự")]
        [MaxLength(45, ErrorMessage = "Tên nhân viênkhông quá 45 kí tự")]
        public String TenNV { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "CMND không được bỏ trống")]
        [MinLength(9, ErrorMessage = "CMND tối thiểu 9 kí tự")]
        [MaxLength(12, ErrorMessage = "CMND tối đa 12 kí tự")]
        public String CMND { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [MinLength(10, ErrorMessage = "Địa chỉ tối thiểu 10 kí tự")]
        [MaxLength(200, ErrorMessage = "Địa chỉ tối đa 200 kí tự")]
        public String DiaChi { get; set; }

        [Display(Name = "SĐT", Description = "SĐT dùng để liên lạc khi cần thiết")]
        [Required(ErrorMessage = "SĐT không được bỏ trống")]
        [MinLength(9, ErrorMessage = "SĐT tối thiểu 9 kí tự")]
        [MaxLength(13, ErrorMessage = "SĐT tối đa 13 kí tự")]
        public String SDT { get; set; }

        public int MaNhom { get; set; }

        [ForeignKey("MaNhom")]
        public Nhom Nhom { get; set; }

        [InverseProperty("NhanVien")]
        public IList<KhachHang> KhachHang { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is NhanVien)
            {
                NhanVien compareObj = (NhanVien)obj;

                if (compareObj.MaNV == MaNV)
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