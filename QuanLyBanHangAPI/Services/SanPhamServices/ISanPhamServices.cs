using QuanLyBanHangAPI.Models.SanPham;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.SanPhamServices
{
    public interface ISanPhamServices
    {
        List<SanPhamVM> GetAll();
        SanPhamVM GetByID(int id);
        SanPhamVM Add(SanPhamModel model);
        void Delete(int id);
        void Update(SanPhamVM vm);
    }
}
