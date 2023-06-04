using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.ChiTietDonHang;
using System;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.ChiTietDonHangServices
{
    public interface IChiTietDonHangServices
    {
        ChiTietDonHangVM Add(ChiTietDonHangModel model);
        List<ChiTietDonHangVM> GetAll();
        ChiTietDonHangVM GetById(Guid id);
        bool Delete(Guid id);
        bool Update(ChiTietDonHangVM vm);
    }
}
