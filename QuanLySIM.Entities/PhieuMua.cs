using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    [Table("PhieuMua")]
    public class PhieuMua
    {
        [Key]
        [ScaffoldColumn(false)]
        public int MaPM { get; set; }

        [Display(Name = "Ngày đặt mua")]
        [Required(ErrorMessage = "Không được bỏ trống Ngày đặt hàng")]
        public DateTime NgayDatMua { get; set; }

        [Display(Name = "Ngày giao hàng")]
        [Required(ErrorMessage = "Không được bỏ trống Ngày giao hàng")]
        public DateTime NgayGiao { get; set; }

        [Display(Name = "Tổng tiền")]
        [Required(ErrorMessage = "Không được bỏ trống Tổng tiền")]
        [Range(1, 9999999, ErrorMessage = "Tổng tiền từ 1.000 đến 9.999.999.000")]
        public decimal TongTien { get; set; }

        public int MaKH { get; set; }

        [ForeignKey("MaKH")]
        public KhachHang KhachHang { get; set; }

        public int SimId { get; set; }

        [ForeignKey("SimId")]
        public SIM SIM { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PhieuMua)
            {
                PhieuMua compareObj = (PhieuMua)obj;

                if (compareObj.MaPM == MaPM)
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