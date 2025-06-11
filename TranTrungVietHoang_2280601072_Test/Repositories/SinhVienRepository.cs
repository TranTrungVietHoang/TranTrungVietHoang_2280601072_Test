using Microsoft.EntityFrameworkCore;
using TranTrungVietHoang_2280601072_Test.Models;
using TranTrungVietHoang_2280601072_Test.Repositories;

public class SinhVienRepository : ISinhVienRepository
{
    private readonly ApplicationDbContext _context;

    public SinhVienRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SinhVien>> GetAllAsync()
    {
        return await _context.SinhViens.Include(s => s.NganhHoc).ToListAsync();
    }

    public async Task<SinhVien> GetByIdAsync(string maSV)
    {
        return await _context.SinhViens.Include(s => s.NganhHoc)
            .FirstOrDefaultAsync(s => s.MaSV == maSV);
    }

    public async Task AddAsync(SinhVien sinhVien)
    {
        _context.SinhViens.Add(sinhVien);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SinhVien sinhVien)
    {
        _context.SinhViens.Update(sinhVien);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string maSV)
    {
        var sv = await _context.SinhViens.FindAsync(maSV);
        if (sv != null)
        {
            _context.SinhViens.Remove(sv);
            await _context.SaveChangesAsync();
        }
    }
}
