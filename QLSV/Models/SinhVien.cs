using System;
using System.Collections.Generic;
using System.ComponentModel;
using QLSV.Resources;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Models
{
    public class SinhVien
    {
        public SinhVien()
        {
            DangKy = new HashSet<DangKy>();
        }
        [StringLength(10), Required]
        [Display(Name ="Mssv", ResourceType = typeof(SinhVienResources))]
        public string Mssv { get; set; }
        [StringLength(30)]
        [Display(Name = "Hoten", ResourceType = typeof(SinhVienResources))]
        public string Hoten { get; set; }

        [StringLength(10)]
        [Display(Name = "Namsinh", ResourceType = typeof(SinhVienResources))]
        public string Namsinh { get; set; }

        [StringLength(1)]
        [Display(Name = "Gioitinh", ResourceType = typeof(SinhVienResources))]
        public string Gioitinh { get; set; }

        [StringLength(11)]
        [Display(Name = "Sdt", ResourceType = typeof(SinhVienResources))]
        public string Sdt { get; set; }

        [StringLength(30)]
        [Display(Name = "Email", ResourceType = typeof(SinhVienResources))]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Diachi", ResourceType = typeof(SinhVienResources))]
        public string Diachi { get; set; }

        [StringLength(20)]
        [Display(Name = "Nganh", ResourceType = typeof(SinhVienResources))]
        public string Nganh { get; set; }

        [StringLength(10)]
        [Display(Name = "Lop", ResourceType = typeof(SinhVienResources))]
        public string Lop { get; set; }
        [Display(Name = "Hinhanh", ResourceType = typeof(SinhVienResources))]
        public string Hinhanh { get; set; }

        public ICollection<DangKy> DangKy { get; set; }
        
    }
}
