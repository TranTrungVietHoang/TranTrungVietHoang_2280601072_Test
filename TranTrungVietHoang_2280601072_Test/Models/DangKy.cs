using System.ComponentModel.DataAnnotations;

namespace TranTrungVietHoang_2280601072_Test.Models
{
    public class DangKy
    {
        [Key]
        public int MaDK { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime NgayDK { get; set; }

        public string MaSV { get; set; }
        public SinhVien SinhVien { get; set; }
        public ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
