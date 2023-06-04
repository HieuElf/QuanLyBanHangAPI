using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Models.DonDatHang;
using System;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.DonDatHangServices
{
    public interface IDonDatHangServices
    {
        DonDatHangVM Add(DonDatHangModel model);
        List<DonDatHangVM> GetAll();
        List<DonDatHangVM> GetAllByUser(string username);
        DonDatHangVM GetByID(Guid id);
        bool Delete(Guid id);
        bool Update(DonDatHangVM vm);
        bool UpdateByUser(UpdateTTDonHangDto dto);
        bool UpdateDonHang(UpdateDonHangDto dto);
    }
}
