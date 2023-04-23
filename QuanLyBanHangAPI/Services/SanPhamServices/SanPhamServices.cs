using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.SanPham;
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
                TomTat = model.TomTat,
                MoTa = model.MoTa,
                HinhAnh = model.HinhAnhUri.FileName,
                TrangThai = model.TrangThai,
            };
            _db.Add(sp);
            _db.SaveChanges();
            return new SanPhamVM
            {
                MaSP = sp.MaSP,
                TenSP = sp.TenSP,
                MaGoi = sp.MaGoi,
                TomTat = sp.TomTat,
                MoTa = sp.MoTa,
                HinhAnh = sp.HinhAnh,
                TrangThai = sp.TrangThai,
            };
        }

        public void Delete(int id)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp != null)
            {
                _db.Remove(sp);
                _db.SaveChanges();
            }
        }

        public SanPhamVM GetByID(int id)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == id);
            if (sp != null)
            {
                return new SanPhamVM
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    MaGoi = sp.MaGoi,
                    TomTat = sp.TomTat,
                    MoTa = sp.MoTa,
                    HinhAnh = sp.HinhAnh,
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
                TomTat =m.TomTat,
                MoTa = m.MoTa,
                HinhAnh = m.HinhAnh,
                TrangThai = m.TrangThai,
            });
            return sps.ToList();
        }

        public void Update(SanPhamVM vm)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.MaSP == vm.MaSP);
            if (sp != null)
            {
                sp.TenSP = vm.TenSP;
                sp.MaGoi = vm.MaGoi;
                sp.TomTat = vm.TomTat;
                sp.MoTa = vm.MoTa;
                sp.HinhAnh = vm.HinhAnh;
                sp.TrangThai = vm.TrangThai;
                _db.SaveChanges();                           
            }
        }

        public SanPhamVM GetByName(string name)
        {
            var sp = _db.SanPhams.SingleOrDefault(m => m.TenSP == name);
            if (sp != null)
            {
                return new SanPhamVM
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    MaGoi = sp.MaGoi,
                    TomTat = sp.TomTat,
                    MoTa = sp.MoTa,
                    HinhAnh = sp.HinhAnh,
                    TrangThai = sp.TrangThai
                };
            }
            return null;
        }
    }
}
