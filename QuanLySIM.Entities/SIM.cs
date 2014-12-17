using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    public class TinhTrangSIM
    {
        public String Value { get; set; }
        public String Text { get; set; }
    }

    [Table("SIM")]
    public class SIM
    {
        public const String SOLD = "Sold";
        public const String AVAILABLE = "Available";
        public const String NOT_PAID = "Not Paid";

        private String _TinhTrang = AVAILABLE;
        private IList<TinhTrangSIM> _CacTinhTrang;

        [Key]
        [ScaffoldColumn(false)]
        public int SimId { get; set; }

        [Display(Name = "Mã số SIM")]
        [Required(ErrorMessage = "Mã SIM không được bỏ trống")]
        [MinLength(16, ErrorMessage = "Mã SIM tối thiểu 16 kí tự")]
        [MaxLength(16, ErrorMessage = "Mã SIM tối đa 16 kí tự")]
        public String MaSIM { get; set; }

        [Display(Name = "Số thuê bao")]
        [Required(ErrorMessage = "Số thuê bao không được bỏ trống")]
        [MinLength(9, ErrorMessage = "Số thuê bao tối thiểu 9 kí tự")]
        [MaxLength(13, ErrorMessage = "Số thuê bao tối đa 13 kí tự")]
        public String SoThueBao { get; set; }

        [Display(Name = "Giá tiền")]
        [Required(ErrorMessage = "Giá tiền không được bỏ trống")]
        [Range(0, 9999999, ErrorMessage = "Giá tiền từ 1.000 đến 9.999.999.000")]
        public decimal GiaTien { get; set; }

        [Display(Name = "Tình trạng", Description = "Tình trạng đơn hàng: đã mua hoặc chưa,..")]
        [Required(ErrorMessage = "Tình trạng không được bỏ trống")]
        [MinLength(3, ErrorMessage = "Tình trạng tối thiểu 3 kí tự")]
        [MaxLength(20, ErrorMessage = "Tình trạng tối đa 20 kí tự")]
        public String TinhTrang
        {
            get { return _TinhTrang; }
            set { _TinhTrang = value; }
        }

        [NotMapped]
        public IList<TinhTrangSIM> CacTinhTrang
        {
            get
            {
                if (_CacTinhTrang != null)
                    return _CacTinhTrang;

                _CacTinhTrang = new List<TinhTrangSIM>();
                _CacTinhTrang.Add(new TinhTrangSIM { Value = SIM.SOLD, Text = "Đã Bán" });
                _CacTinhTrang.Add(new TinhTrangSIM { Value = SIM.AVAILABLE, Text = "Còn Hàng" });
                _CacTinhTrang.Add(new TinhTrangSIM { Value = SIM.NOT_PAID, Text = "Chưa Thanh Toán" });

                return _CacTinhTrang;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is SIM)
            {
                SIM compareObj = (SIM)obj;

                if (compareObj.SimId == SimId)
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