using TranTrungVietHoang_2280601072_Test.Models;

namespace TranTrungVietHoang_2280601072_Test.Repositories
{
    public interface ISinhVienRepository
    {
        Task<IEnumerable<SinhVien>> GetAllAsync();
        Task<SinhVien> GetByIdAsync(string id);
        Task AddAsync(SinhVien sv);
        Task UpdateAsync(SinhVien sv);
        Task DeleteAsync(string id);
    }
}
