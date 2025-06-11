using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TranTrungVietHoang_2280601072_Test.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Hình ảnh")]
        
        public string Hinh { get; set; }

        [Display(Name = "Ngành học")]
        public string MaNganh { get; set; }

         
        public NganhHoc NganhHoc { get; set; }

        
        public ICollection<DangKy> DangKys { get; set; }
    }
}
