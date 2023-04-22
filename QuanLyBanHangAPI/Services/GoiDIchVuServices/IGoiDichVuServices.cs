using QuanLyBanHangAPI.Models.GoiDichVu;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.GoiDIchVuServices
{
    public interface IGoiDichVuServices
    {
        GoiDichVuVM Add(GoiDichVuModel model);
        List<GoiDichVuVM> GetAll();
        GoiDichVuVM GetById(int id);
        void Update(GoiDichVuVM vm);
        void Delete(int id);
    }
}
