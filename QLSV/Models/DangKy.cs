using QLSV.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Models
{
    public partial class DangKy
    {
        [StringLength(20), Required]
        [Display(Name = "Mahp", ResourceType = typeof(DangKyResources))]
        public string Mahp { get; set; }
        [StringLength(10), Required]
        [Display(Name = "Mssv", ResourceType = typeof(DangKyResources))]
        public string Mssv { get; set; }

        public virtual HocPhan MahpNavigation { get; set; }
        public virtual SinhVien MssvNavigation { get; set; }
    }
}
