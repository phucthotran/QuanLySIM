using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySIM.Entities
{
    [Table("Role")]
    [ScaffoldTable(false)]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "Vai trò")]
        [Required(ErrorMessage = "Vai trò không được bỏ trống")]
        [MinLength(3, ErrorMessage = "Vai trò tối thiểu 3 kí tự")]
        [MaxLength(20, ErrorMessage = "Vai trò tối đa 20 kí tự")]
        public String Name { get; set; }

        [InverseProperty("Role")]
        public IList<Nhom> Nhom { get; set; }
    }
}