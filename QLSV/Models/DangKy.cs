using QLSV.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSV.Models
{
    public class DangKy
    {
        [StringLength(20), Required]
        [Display(Name = "Mahp", ResourceType = typeof(DangKyResources))]
        public string Mahp { get; set; }
        [StringLength(10), Required]
        [Display(Name = "Mssv", ResourceType = typeof(DangKyResources))]
        public string Mssv { get; set; }
        [ForeignKey("Mahp")]
        public HocPhan MahpNavigation { get; set; }
        [ForeignKey("Mssv")]
        public SinhVien MssvNavigation { get; set; }
    }
}
