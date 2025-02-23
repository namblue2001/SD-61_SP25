using StyleTee.Models;
using StyleTee.Repository;
using StyleTee.Service.IService;

namespace StyleTee.Service.Service
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly IAllRepositories<TaiKhoan> _taikhoanrepos;

        public TaiKhoanService(IAllRepositories<TaiKhoan> taikhoanrepos)
        {
            _taikhoanrepos = taikhoanrepos;
        }

        public TaiKhoan Register(TaiKhoan taikhoan)
        {
            if (_taikhoanrepos.FindBy(u => u.taiKhoan == taikhoan.taiKhoan) != null)
            {
                throw new Exception("Username already exists.");
            }
            _taikhoanrepos.Insert(taikhoan);
            _taikhoanrepos.Save();
            return taikhoan;
        }
    }
}
