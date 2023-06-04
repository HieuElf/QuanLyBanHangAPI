using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Data.DTO;
using QuanLyBanHangAPI.Models.DonDatHang;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.DonDatHangServices
{
    public class DonDatHangServices : IDonDatHangServices
    {
        private readonly DB _db;
        public DonDatHangServices(DB db)
        {
            _db = db;
        }

        public DonDatHangVM Add(DonDatHangModel model)
        {
            var donhang = new DonDatHang
            {
                MaKhachHang = model.MaKhachHang,
                TenKhachHang = model.TenKhachHang,
                Email = model.Email,
                DiaChi = model.DiaChi,
                SoDienThoai = model.SoDienThoai,
                NgayDatHang = DateTime.Now,
                MaDonViChuyenPhat = model.MaDonViChuyenPhat,
                TinhTrangGiaoHang = model.TinhTrangGiaoHang,
                NgayGiao = DateTime.Now.AddDays(3),
                MaGiaoDichVNPay = model.MaGiaoDichVNPay,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                TrangThaiDonHang = model.TrangThaiDonHang
            };
            _db.Add(donhang);
            _db.SaveChanges();
            return new DonDatHangVM
            {
                MaDonHang = donhang.MaDonHang,
                MaKhachHang = donhang.MaKhachHang,
                TenKhachHang = donhang.TenKhachHang,
                Email = donhang.Email,
                DiaChi = donhang.DiaChi,
                SoDienThoai = donhang.SoDienThoai,
                NgayDatHang = donhang.NgayDatHang,
                MaDonViChuyenPhat = donhang.MaDonViChuyenPhat,
                TinhTrangGiaoHang = donhang.TinhTrangGiaoHang,
                NgayGiao = donhang.NgayGiao,
                MaGiaoDichVNPay = donhang.MaGiaoDichVNPay,
                PhuongThucThanhToan = donhang.PhuongThucThanhToan,
                TrangThaiDonHang = donhang.TrangThaiDonHang
            };
        }

        public bool Delete(Guid id)
        {
            var donhang = _db.DonDatHangs.SingleOrDefault(m => m.MaDonHang == id);
            if (donhang != null)
            {
                _db.Remove(donhang);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<DonDatHangVM> GetAll()
        {
            var donhangs = _db.DonDatHangs.Select(m => new DonDatHangVM
            {
                MaDonHang = m.MaDonHang,
                MaKhachHang = m.MaKhachHang,
                TenKhachHang = m.TenKhachHang,
                Email = m.Email,
                DiaChi = m.DiaChi,
                SoDienThoai = m.SoDienThoai,
                NgayDatHang = m.NgayDatHang,
                MaDonViChuyenPhat = m.MaDonViChuyenPhat,
                TinhTrangGiaoHang = m.TinhTrangGiaoHang,
                NgayGiao = m.NgayGiao,
                MaGiaoDichVNPay = m.MaGiaoDichVNPay,
                PhuongThucThanhToan = m.PhuongThucThanhToan,
                TrangThaiDonHang = m.TrangThaiDonHang
            });
            return donhangs.ToList();
        }

        public List<DonDatHangVM> GetAllByUser(string username)
        {
            var donhangs = _db.DonDatHangs
                    .Where(m => m.MaKhachHang == username)
                    .ToList();
            var vm = new List<DonDatHangVM>();
            foreach (var item in donhangs)
            {
                var donhangvm = new DonDatHangVM
                {
                    MaDonHang = item.MaDonHang,
                    MaKhachHang = item.MaKhachHang,
                    TenKhachHang = item.TenKhachHang,
                    Email = item.Email,
                    DiaChi = item.DiaChi,
                    SoDienThoai = item.SoDienThoai,
                    NgayDatHang = item.NgayDatHang,
                    MaDonViChuyenPhat = item.MaDonViChuyenPhat,
                    TinhTrangGiaoHang = item.TinhTrangGiaoHang,
                    NgayGiao = item.NgayGiao,
                    MaGiaoDichVNPay = item.MaGiaoDichVNPay,
                    PhuongThucThanhToan = item.PhuongThucThanhToan,
                    TrangThaiDonHang = item.TrangThaiDonHang
                };
                vm.Add(donhangvm);
            }
            return vm;
        }

        public DonDatHangVM GetByID(Guid id)
        {
            var donhang = _db.DonDatHangs.SingleOrDefault(m => m.MaDonHang == id);
            if (donhang != null)
            {
                return new DonDatHangVM
                {
                    MaDonHang = donhang.MaDonHang,
                    MaKhachHang = donhang.MaKhachHang,
                    TenKhachHang = donhang.TenKhachHang,
                    Email = donhang.Email,
                    DiaChi = donhang.DiaChi,
                    SoDienThoai = donhang.SoDienThoai,
                    NgayDatHang = donhang.NgayDatHang,
                    MaDonViChuyenPhat = donhang.MaDonViChuyenPhat,
                    TinhTrangGiaoHang = donhang.TinhTrangGiaoHang,
                    NgayGiao = donhang.NgayGiao,
                    MaGiaoDichVNPay = donhang.MaGiaoDichVNPay,
                    PhuongThucThanhToan = donhang.PhuongThucThanhToan,
                    TrangThaiDonHang = donhang.TrangThaiDonHang
                };
            }
            return null;
        }

        public bool Update(DonDatHangVM vm)
        {
            var donhang = _db.DonDatHangs.SingleOrDefault(m => m.MaDonHang == vm.MaDonHang);
            if (donhang != null)
            {
                donhang.MaKhachHang = vm.MaKhachHang;
                donhang.TenKhachHang = vm.TenKhachHang;
                donhang.Email = vm.Email;
                donhang.DiaChi = vm.DiaChi;
                donhang.SoDienThoai = vm.SoDienThoai;
                donhang.NgayDatHang = vm.NgayDatHang;
                donhang.MaDonViChuyenPhat = vm.MaDonViChuyenPhat;
                donhang.TinhTrangGiaoHang = vm.TinhTrangGiaoHang;
                donhang.NgayGiao = vm.NgayGiao;
                donhang.MaGiaoDichVNPay = vm.MaGiaoDichVNPay;
                donhang.PhuongThucThanhToan = vm.PhuongThucThanhToan;
                donhang.TrangThaiDonHang = vm.TrangThaiDonHang;
                _db.Update(donhang);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateByUser(UpdateTTDonHangDto dto)
        {
            var donhang = _db.DonDatHangs.SingleOrDefault(m => m.MaDonHang== dto.MaDonHang);
            if (donhang != null)
            {
                donhang.MaGiaoDichVNPay = dto.MaGiaoDichVNPay;
                donhang.TrangThaiDonHang = "Đã thanh toán";
                _db.Update(donhang);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateDonHang(UpdateDonHangDto dto)
        {
            var donhang = _db.DonDatHangs.SingleOrDefault(m => m.MaDonHang == dto.MaDonHang);
            if (donhang != null)
            {
                if (dto.RequestCode == 0)
                {
                    donhang.TrangThaiDonHang = "Đã hủy";
                    donhang.TinhTrangGiaoHang = "Đã hủy";
                    _db.Update(donhang);
                    _db.SaveChanges();
                    return true;
                }
                if (dto.RequestCode == 1)
                {
                    donhang.TrangThaiDonHang = "Đã hoàn thành";
                    donhang.TinhTrangGiaoHang = "Đã giao hàng";
                    donhang.NgayGiao = DateTime.Now;
                    _db.Update(donhang);
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
