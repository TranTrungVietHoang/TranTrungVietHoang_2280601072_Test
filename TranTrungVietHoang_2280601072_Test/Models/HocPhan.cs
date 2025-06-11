using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranTrungVietHoang_2280601072_Test.Models
{
    [Table("HocPhan")]
    public class HocPhan
    {
        [Key]
        public string MaHP { get; set; }

        [Display(Name = "Tên học phần")]
        public string TenHP { get; set; }

        [Display(Name = "Số tín chỉ")]
        public int SoTinChi { get; set; }

        [Display(Name = "Số lượng dự kiến")]
        public int SoLuongDuKien { get; set; }

        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
