using QLSV.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLSV.Models
{
    public class HocPhan
    {
        public HocPhan()
        {
            DangKy = new HashSet<DangKy>();
        }
        [StringLength(20), Required]
        [Display(Name = "Mahp", ResourceType = typeof(HocPhanResources))]
        public string Mahp { get; set; }

        [StringLength(30)]
        [Display(Name = "Tenmon", ResourceType = typeof(HocPhanResources))]
        public string Tenmon { get; set; }

        [Display(Name = "Siso", ResourceType = typeof(HocPhanResources))]
        public int? Siso { get; set; }

        [StringLength(5)]
        [Display(Name = "Phonghoc", ResourceType = typeof(HocPhanResources))]
        public string Phonghoc { get; set; }

        [StringLength(5)]
        [Display(Name = "Tiethoc", ResourceType = typeof(HocPhanResources))]
        public string Tiethoc { get; set; }

        [StringLength(1)]
        [Display(Name = "Thu", ResourceType = typeof(HocPhanResources))]
        public string Thu { get; set; }

        [Display(Name = "Ngaybd", ResourceType = typeof(HocPhanResources))]

        [StringLength(10)]
        public string Ngaybd { get; set; }

        [StringLength(10)]
        [Display(Name = "Ngaykt", ResourceType = typeof(HocPhanResources))]
        public string Ngaykt { get; set; }

        public ICollection<DangKy> DangKy { get; set; }
    }
}
