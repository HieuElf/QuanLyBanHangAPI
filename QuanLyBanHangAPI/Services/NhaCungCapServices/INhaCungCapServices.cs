using QuanLyBanHangAPI.Models.NhaCungCap;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.NhaCungCapServices
{
    public interface INhaCungCapServices
    {
        List<NhaCungCapVM> GetAll();
        NhaCungCapVM GetById(int id);
        NhaCungCapVM GetByName(string name);
        NhaCungCapVM Add(NhaCungCapModel model);
        void Update(NhaCungCapVM vm);
        void DeleteById(int id);

    }
}
