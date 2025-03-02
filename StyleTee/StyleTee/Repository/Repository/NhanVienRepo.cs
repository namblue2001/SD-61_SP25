using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using StyleTee.Data;
using StyleTee.Models;
using StyleTee.Repository.IRepoitory;

namespace StyleTee.Repository.Repository
{
    public class NhanVienRepo : INhanVienRepo
    {
        private readonly ApplicationDbContext _dbcontext;

        public NhanVienRepo(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(TaiKhoan nhanvien)
        {
            _dbcontext.TaiKhoan.Add(nhanvien);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var staff = await _dbcontext.TaiKhoan.FirstOrDefaultAsync(x => x.ID_TaiKhoan == id);
            if (staff != null)
            {
                _dbcontext.TaiKhoan.Remove(staff);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaiKhoan>> GetAllAsync()
        {
            return await _dbcontext.TaiKhoan.ToListAsync();
        }

        public async Task<TaiKhoan> GetByIdAsync(Guid id)
        {

            return await _dbcontext.TaiKhoan.FirstOrDefaultAsync(x => x.ID_TaiKhoan == id);
        }

        public async Task UpdateAsync(TaiKhoan nhanvien)
        {
            _dbcontext.TaiKhoan.Update(nhanvien);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
