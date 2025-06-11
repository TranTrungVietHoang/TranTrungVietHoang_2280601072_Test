using Microsoft.EntityFrameworkCore;
using TranTrungVietHoang_2280601072_Test.Models;
using TranTrungVietHoang_2280601072_Test.Repositories;

public class NganhHocRepository : INganhHocRepository
{
    private readonly ApplicationDbContext _context;

    public NganhHocRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NganhHoc>> GetAllAsync()
    {
        return await _context.NganhHocs.ToListAsync();
    }
}
