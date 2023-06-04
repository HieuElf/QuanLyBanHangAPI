using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.SanPham;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.SanPhamServices
{
    public class SanPhamServices : ISanPhamServices
    {
        private readonly DB _db;
        public SanPhamServices(DB db)
        {
            _db = db;
        }

        public SanPhamVM Add(SanPhamModel model)
        {
            var sp = new SanPham
            {
                TenSP = model.TenSP,
                MaGoi = model.MaGoi,
                MaNhaCungCap= model.MaNhaCungCap,
                TomTat = model.TomTat,
                MoTa = model.MoTa,
                AnhSP = model.AnhSP,
                ListAnh = model.ListAnh,
                NoiDung = model.NoiDung,
                LuotXem = 0,
                DaBan = 0,
                Gia = model.Gia,
                GiamGia = model.GiamGia,
                NgayTao = DateTime.Now,
                NgayChinhSuaCuoi = DateTime.Now,
                TrangThai = model.TrangThai,
            };
            _db.Add(sp);
            _db.SaveChanges();
            return new SanPhamVM
            {
                MaSP = sp.MaSP,
                TenSP = sp.TenSP,
                MaGoi = sp.MaGoi,
                MaNhaCungCap = sp.MaNhaCungCap,
                TomTat = sp.TomTat,
                MoTa = sp.MoTa,
                AnhSP = sp.AnhSP,
                ListAnh = sp.ListAnh,
                NoiDung = sp.NoiDung,
                LuotXem = sp.LuotXem,
                DaBan = sp.DaBan,
                Gia = sp.Gia,   
                GiamGia = sp.GiamGia,
                NgayChinhSuaCuoi = sp.NgayChinhSuaCuoi,
                NgayTao = sp.NgayTao,
                TrangThai = sp.TrangThai,
            };
        }

        public bool Delete(int id)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp != null)
            {
                _db.Remove(sp);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public SanPhamVM GetByID(int id)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp != null)
            {
                sp.LuotXem++;
                _db.Update(sp);
                _db.SaveChanges();
                return new SanPhamVM
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    MaGoi = sp.MaGoi,
                    MaNhaCungCap = sp.MaNhaCungCap,
                    TomTat = sp.TomTat,
                    MoTa = sp.MoTa,
                    AnhSP = sp.AnhSP,
                    ListAnh = sp.ListAnh,
                    NoiDung = sp.NoiDung,
                    LuotXem = sp.LuotXem,
                    DaBan = sp.DaBan,
                    Gia = sp.Gia,
                    GiamGia = sp.GiamGia,
                    NgayTao = sp.NgayTao,
                    NgayChinhSuaCuoi = sp.NgayChinhSuaCuoi,
                    TrangThai = sp.TrangThai
                };
            }
            return null;
        }

        public List<SanPhamVM> GetAll()
        {
            var sps = _db.SanPhams.Select(m => new SanPhamVM
            {
                MaSP = m.MaSP,
                TenSP = m.TenSP,
                MaGoi = m.MaGoi,
                MaNhaCungCap = m.MaNhaCungCap,
                TomTat = m.TomTat,
                MoTa = m.MoTa,
                AnhSP = m.AnhSP,
                ListAnh = m.ListAnh,
                NoiDung = m.NoiDung,
                LuotXem = m.LuotXem,
                DaBan = m.DaBan,
                Gia = m.Gia,
                GiamGia = m.GiamGia,
                NgayChinhSuaCuoi = m.NgayChinhSuaCuoi,
                NgayTao = m.NgayTao,
                TrangThai = m.TrangThai,
            });
            return sps.ToList();
        }

        public string Update(SanPhamVM vm)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == vm.MaSP);
            if (sp != null)
            {
                var duplicate = _db.SanPhams
                    .Where(m => m.TenSP == vm.TenSP && m.MaSP != vm.MaSP)
                    .ToList();
                if (duplicate.Any())
                {
                    return "Đã tồn tại dữ liệu khác trùng tên";
                }
                sp.TenSP = vm.TenSP;
                sp.MaGoi = vm.MaGoi;
                sp.MaNhaCungCap = vm.MaNhaCungCap;
                sp.TomTat = vm.TomTat;
                sp.MoTa = vm.MoTa;
                sp.AnhSP = vm.AnhSP;
                sp.ListAnh = vm.ListAnh;
                sp.TrangThai = vm.TrangThai;
                sp.NoiDung = vm.NoiDung;
                sp.LuotXem = vm.LuotXem;
                sp.DaBan = vm.DaBan;
                sp.GiamGia = vm.GiamGia;
                sp.Gia = vm.Gia;
                sp.NgayChinhSuaCuoi = DateTime.Now;
                _db.Update(sp);
                _db.SaveChanges();
                return "OK";
            }
            return "Không tồn tại";
        }

        public SanPhamVM GetByName(string name)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.TenSP == name);
            if (sp != null)
            {
                sp.LuotXem++;
                _db.SaveChanges();
                return new SanPhamVM
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    MaGoi = sp.MaGoi,
                    MaNhaCungCap = sp.MaNhaCungCap,
                    TomTat = sp.TomTat,
                    MoTa = sp.MoTa,
                    AnhSP = sp.AnhSP,
                    ListAnh = sp.ListAnh,
                    NoiDung = sp.NoiDung,
                    LuotXem = sp.LuotXem,
                    DaBan = sp.DaBan,
                    Gia = sp.Gia,
                    GiamGia = sp.GiamGia,
                    NgayTao = sp.NgayTao,
                    NgayChinhSuaCuoi = sp.NgayChinhSuaCuoi,
                    TrangThai = sp.TrangThai
                };
            }
            return null;
        }
    }
}
