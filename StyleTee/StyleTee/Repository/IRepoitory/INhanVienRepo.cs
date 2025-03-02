using StyleTee.Models;

namespace StyleTee.Repository.IRepoitory
{
    public interface INhanVienRepo
    {
        Task<IEnumerable<TaiKhoan>> GetAllAsync();
        Task<TaiKhoan> GetByIdAsync(Guid id);
        Task AddAsync(TaiKhoan nhanvien);
        Task UpdateAsync(TaiKhoan nhanvien);
        Task DeleteAsync(Guid id);
    }
}
