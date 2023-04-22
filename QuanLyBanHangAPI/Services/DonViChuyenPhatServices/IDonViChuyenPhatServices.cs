using QuanLyBanHangAPI.Models.DonViChuyenPhat;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.DonViChuyenPhatServices
{
    public interface IDonViChuyenPhatServices
    {
        List<DonViChuyenPhatVM> GetAll();
        DonViChuyenPhatVM GetByID(int id);
        DonViChuyenPhatVM Add(DonViChuyenPhatModel model);
        void Update(DonViChuyenPhatVM vm);
        void Delete(int id);
    }
}
