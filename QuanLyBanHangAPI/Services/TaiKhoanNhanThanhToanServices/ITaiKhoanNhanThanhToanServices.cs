using QuanLyBanHangAPI.Models.TaiKhoanNhanThanhToan;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.TaiKhoanNhanThanhToanServices
{
    public interface ITaiKhoanNhanThanhToanServices
    {
        List<TaiKhoanNhanThanhToanVM> GetAll();
        TaiKhoanNhanThanhToanVM GetById(int id);
        TaiKhoanNhanThanhToanVM GetByName(string name);
        TaiKhoanNhanThanhToanVM Add(TaiKhoanNhanThanhToanModel model);
        void Update(TaiKhoanNhanThanhToanVM vm);
        void Delete(int id);
    }
}
