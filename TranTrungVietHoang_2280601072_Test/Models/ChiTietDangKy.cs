using System.ComponentModel.DataAnnotations.Schema;

namespace TranTrungVietHoang_2280601072_Test.Models
{
    [Table("ChiTietDangKy")]
    public class ChiTietDangKy
    {
        public int MaDK { get; set; }
        public string MaHP { get; set; }

        public DangKy DangKy { get; set; }
        public HocPhan HocPhan { get; set; }
    }
}
