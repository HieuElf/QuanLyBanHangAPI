using QuanLyBanHangAPI.Models.SanPham;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.SanPhamServices
{
    public interface ISanPhamServices
    {
        List<SanPhamVM> GetAll();
        SanPhamVM GetByID(int id);
        SanPhamVM GetByName(string name);
        SanPhamVM Add(SanPhamModel model);
        bool Delete(int id);
        string Update(SanPhamVM vm);
    }
}
