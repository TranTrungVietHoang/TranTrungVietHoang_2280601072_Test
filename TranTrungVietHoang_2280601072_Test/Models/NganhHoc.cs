using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranTrungVietHoang_2280601072_Test.Models
{
    [Table("NganhHoc")]
    public class NganhHoc
    {
        [Key]
        public string MaNganh { get; set; }
        public string TenNganh { get; set; }
        public ICollection<SinhVien> SinhViens { get; set; }
    }
}
