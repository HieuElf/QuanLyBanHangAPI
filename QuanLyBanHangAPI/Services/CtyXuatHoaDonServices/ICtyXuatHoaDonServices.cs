using QuanLyBanHangAPI.Models.CtyXuatHoaDon;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.CtyXuatHoaDonServices
{
    public interface ICtyXuatHoaDonServices
    {
        List<CtyXuatHoaDonVM> GetAll();
        CtyXuatHoaDonVM GetById(int id);
        CtyXuatHoaDonVM GetByName(string name);
        CtyXuatHoaDonVM Add(CtyXuatHoaDonModel model);
        void DeleteById(int id);
        void Update(CtyXuatHoaDonVM vm);
    }
}
