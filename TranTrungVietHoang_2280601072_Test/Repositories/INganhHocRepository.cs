using TranTrungVietHoang_2280601072_Test.Models;

namespace TranTrungVietHoang_2280601072_Test.Repositories
{
    public interface INganhHocRepository
    {
        Task<IEnumerable<NganhHoc>> GetAllAsync();
    }
}
