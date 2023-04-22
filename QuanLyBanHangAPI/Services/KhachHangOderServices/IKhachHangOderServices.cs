using QuanLyBanHangAPI.Models.KhachHangOder;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.KhachHangOderServices
{
    public interface IKhachHangOderServices
    {
        List<KhachHangOderVM> GetAll();
        KhachHangOderVM GetById(int id);
        KhachHangOderVM GetByMST(string mst);
        KhachHangOderVM Add(KhachHangOderModel model);
        void Update(KhachHangOderVM vm);
        void Delete(int id);
    }
}
