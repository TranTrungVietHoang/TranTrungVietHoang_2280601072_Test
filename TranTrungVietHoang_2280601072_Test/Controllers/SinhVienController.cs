using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TranTrungVietHoang_2280601072_Test.Models;
using TranTrungVietHoang_2280601072_Test.Repositories;

namespace TranTrungVietHoang_2280601072_Test.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly INganhHocRepository _nganhHocRepository;
        private readonly IWebHostEnvironment _env;

        public SinhVienController(ISinhVienRepository sinhVienRepo, INganhHocRepository nganhHocRepo, IWebHostEnvironment env)
        {
            _sinhVienRepository = sinhVienRepo;
            _nganhHocRepository = nganhHocRepo;
            _env = env;
        }

        // Hiển thị danh sách sinh viên
        public async Task<IActionResult> Index(string search)
        {
            var sinhViens = await _sinhVienRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                sinhViens = sinhViens.Where(s => s.HoTen.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            return View(sinhViens);
        }

        // GET: Add
        public async Task<IActionResult> Add()
        {
            var nganhList = await _nganhHocRepository.GetAllAsync();
            ViewBag.Nganhs = new SelectList(nganhList, "MaNganh", "TenNganh");
            return View();
        }

        // POST: Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SinhVien sinhVien, IFormFile HinhUpload)
        {
            if (!ModelState.IsValid)
            {
                //TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin nhập vào.";
                //var list = await _nganhHocRepository.GetAllAsync();
                //ViewBag.Nganhs = new SelectList(list, "MaNganh", "TenNganh", sinhVien.MaNganh);
                //return View(sinhVien);
                TempData["ErrorMessage"] = "Lỗi dữ liệu:<br>" + string.Join("<br>", ModelState.Where(m => m.Value.Errors.Count > 0)
                    .SelectMany(m => m.Value.Errors.Select(e => $"{m.Key}: {e.ErrorMessage}")));

                var list = await _nganhHocRepository.GetAllAsync();
                ViewBag.Nganhs = new SelectList(list, "MaNganh", "TenNganh", sinhVien.MaNganh);
                return View(sinhVien);
            }

            // Check trùng mã sinh viên
            var existing = await _sinhVienRepository.GetByIdAsync(sinhVien.MaSV);
            if (existing != null)
            {
                TempData["ErrorMessage"] = $"Mã sinh viên {sinhVien.MaSV} đã tồn tại.";
                var list = await _nganhHocRepository.GetAllAsync();
                ViewBag.Nganhs = new SelectList(list, "MaNganh", "TenNganh", sinhVien.MaNganh);
                return View(sinhVien);
            }

            try
            {
                if (HinhUpload != null && HinhUpload.Length > 0)
                {
                    sinhVien.Hinh = await SaveImage(HinhUpload);
                }
                else
                {
                    sinhVien.Hinh = "/images/sinhvien/default.png"; // fallback ảnh mặc định nếu không chọn ảnh
                }

                await _sinhVienRepository.AddAsync(sinhVien);
                TempData["SuccessMessage"] = "Thêm sinh viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi hệ thống: {ex.Message}";
                var list = await _nganhHocRepository.GetAllAsync();
                ViewBag.Nganhs = new SelectList(list, "MaNganh", "TenNganh", sinhVien.MaNganh);
                return View(sinhVien);
            }
        }

        // GET: Update
        public async Task<IActionResult> Update(string id)
        {
            var sv = await _sinhVienRepository.GetByIdAsync(id);
            if (sv == null) return NotFound();

            var nganhList = await _nganhHocRepository.GetAllAsync();
            ViewBag.Nganhs = new SelectList(nganhList, "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        // POST: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, SinhVien sinhVien, IFormFile HinhUpload)
        {
            if (id != sinhVien.MaSV) return NotFound();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ.";
                var nganhList = await _nganhHocRepository.GetAllAsync();
                ViewBag.Nganhs = new SelectList(nganhList, "MaNganh", "TenNganh", sinhVien.MaNganh);
                return View(sinhVien);
            }

            try
            {
                var existing = await _sinhVienRepository.GetByIdAsync(id);
                if (existing == null) return NotFound();

                // Nếu có ảnh mới
                if (HinhUpload != null && HinhUpload.Length > 0)
                {
                    existing.Hinh = await SaveImage(HinhUpload);
                }

                // Cập nhật dữ liệu
                existing.HoTen = sinhVien.HoTen;
                existing.GioiTinh = sinhVien.GioiTinh;
                existing.NgaySinh = sinhVien.NgaySinh;
                existing.MaNganh = sinhVien.MaNganh;

                await _sinhVienRepository.UpdateAsync(existing);
                TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi cập nhật: {ex.Message}";
                return View(sinhVien);
            }
        }

        // GET: Display
        public async Task<IActionResult> Display(string id)
        {
            var sv = await _sinhVienRepository.GetByIdAsync(id);
            if (sv == null) return NotFound();
            return View(sv);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(string id)
        {
            var sv = await _sinhVienRepository.GetByIdAsync(id);
            if (sv == null) return NotFound();
            return View(sv);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _sinhVienRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xóa: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        // Lưu hình ảnh vào wwwroot/images/sinhvien
        private async Task<string> SaveImage(IFormFile image)
        {
            var folder = Path.Combine(_env.WebRootPath, "images/sinhvien");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/sinhvien/" + fileName;
        }
    }
}
