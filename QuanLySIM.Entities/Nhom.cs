using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    [Table("Nhom")]
    public class Nhom
    {
        [Key]
        [ScaffoldColumn(false)]
        public int MaNhom { get; set; }

        [Display(Name = "Tên nhóm")]
        [Required(ErrorMessage = "Tên nhóm không được bỏ trống")]
        [MinLength(3, ErrorMessage = "Tên nhóm tối thiểu 3 kí tự")]
        [MaxLength(20, ErrorMessage = "Tên nhóm tối đa 20 kí tự")]
        public String Ten { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả không được bỏ trống")]
        [MinLength(10, ErrorMessage = "Mô tả tối thiểu 10 kí tự")]
        [MaxLength(100, ErrorMessage = "Mô tả tối đa 100 kí tự")]
        public String MoTa { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [InverseProperty("Nhom")]
        public IList<NhanVien> NhanVien { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Nhom)
            {
                Nhom compareObj = (Nhom)obj;

                if (compareObj.MaNhom == MaNhom)
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