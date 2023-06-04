using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.ChiTietDonHang;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.ChiTietDonHangServices
{
    public class ChiTietDonHangServices : IChiTietDonHangServices
    {
        private readonly DB _db;
        public ChiTietDonHangServices(DB db)
        {
            _db = db;
        }
        public ChiTietDonHangVM Add(ChiTietDonHangModel model)
        {
            var chitiet = new ChiTietDonHang
            {
                MaDonHang = model.MaDonHang,
                MaSP = model.MaSP,
                TenSP= model.TenSP,
                SoLuong = model.SoLuong,
                DonGia= model.DonGia,
                ThanhTien= model.ThanhTien
            };
            _db.Add(chitiet);
            _db.SaveChanges();
            return new ChiTietDonHangVM
            {
                MaChiTietDonHang = chitiet.MaChiTietDonHang,
                MaDonHang = chitiet.MaDonHang,
                MaSP = chitiet.MaSP,
                TenSP = chitiet.TenSP,
                SoLuong = chitiet.SoLuong,
                DonGia = chitiet.DonGia,
                ThanhTien = chitiet.ThanhTien
            };
        }

        public bool Delete(Guid id)
        {
            var chitietdon = _db.ChiTietDonHangs.SingleOrDefault(m => m.MaDonHang == id);
            if (chitietdon != null)
            {
                _db.Remove(chitietdon);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ChiTietDonHangVM> GetAll()
        {
            var chitiets = _db.ChiTietDonHangs.Select(m => new ChiTietDonHangVM
            {
                MaChiTietDonHang = m.MaChiTietDonHang,
                MaDonHang = m.MaDonHang,
                MaSP = m.MaSP,
                TenSP = m.TenSP,
                SoLuong = m.SoLuong,
                DonGia = m.DonGia,
                ThanhTien= m.ThanhTien
            });
            return chitiets.ToList();
        }

        public ChiTietDonHangVM GetById(Guid id)
        {
            var chitietdon = _db.ChiTietDonHangs.SingleOrDefault(m => m.MaDonHang == id);
            if (chitietdon != null)
            {
                return new ChiTietDonHangVM
                {
                    MaChiTietDonHang = chitietdon.MaChiTietDonHang,
                    MaDonHang = chitietdon.MaDonHang,
                    MaSP = chitietdon.MaSP,
                    TenSP = chitietdon.TenSP,
                    SoLuong = chitietdon.SoLuong,
                    DonGia = chitietdon.DonGia,
                    ThanhTien = chitietdon.ThanhTien
                };
            }
            return null;
        }

        public bool Update(ChiTietDonHangVM vm)
        {
            var chitietdon = _db.ChiTietDonHangs.SingleOrDefault(m => m.MaChiTietDonHang == vm.MaChiTietDonHang);
            if (chitietdon != null)
            {
                chitietdon.MaDonHang = vm.MaDonHang;
                chitietdon.MaSP = vm.MaSP;
                chitietdon.TenSP = vm.TenSP;
                chitietdon.SoLuong = vm.SoLuong;
                chitietdon.DonGia = vm.DonGia;
                chitietdon.ThanhTien = vm.ThanhTien;
                _db.Update(chitietdon);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
