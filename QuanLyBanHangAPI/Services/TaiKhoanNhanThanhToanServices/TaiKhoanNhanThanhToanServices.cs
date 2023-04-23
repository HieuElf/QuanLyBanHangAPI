using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.TaiKhoanNhanThanhToan;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.TaiKhoanNhanThanhToanServices
{
    public class TaiKhoanNhanThanhToanServices : ITaiKhoanNhanThanhToanServices
    {
        private readonly DB _db;
        public TaiKhoanNhanThanhToanServices(DB db)
        {
            _db = db;
        }

        public TaiKhoanNhanThanhToanVM Add(TaiKhoanNhanThanhToanModel model)
        {
            var tk = new TaiKhoanNhanThanhToan
            {
                TenTKNhan = model.TenTKNhan,
                STKNhan = model.STKNhan,
                NganHang = model.NganHang,
                ChiNhanh = model.ChiNhanh
            };
            _db.Add(tk);
            _db.SaveChanges();
            return new TaiKhoanNhanThanhToanVM
            {
                IdTK = tk.IdTK,
                TenTKNhan = tk.TenTKNhan,
                STKNhan = tk.STKNhan,
                NganHang = model.NganHang,
                ChiNhanh = model.ChiNhanh
            };
        }

        public void Delete(int id)
        {
            var tk = _db.TaiKhoanNhanThanhToans.SingleOrDefault(m => m.IdTK == id);
            if (tk != null)
            {
                _db.Remove(tk);
                _db.SaveChanges();
            }
        }

        public List<TaiKhoanNhanThanhToanVM> GetAll()
        {
            var tks = _db.TaiKhoanNhanThanhToans.Select(m => new TaiKhoanNhanThanhToanVM
            {
                IdTK = m.IdTK,
                TenTKNhan = m.TenTKNhan,
                STKNhan = m.STKNhan,
                NganHang = m.NganHang,
                ChiNhanh = m.ChiNhanh
            });
            return tks.ToList();
        }

        public TaiKhoanNhanThanhToanVM GetById(int id)
        {
            var tk = _db.TaiKhoanNhanThanhToans.SingleOrDefault(m => m.IdTK == id);
            if (tk != null)
            {
                return new TaiKhoanNhanThanhToanVM
                {
                    IdTK = tk.IdTK,
                    TenTKNhan = tk.TenTKNhan,
                    STKNhan = tk.STKNhan,
                    NganHang = tk.NganHang,
                    ChiNhanh = tk.ChiNhanh
                };
            }
            return null;
        }

        public TaiKhoanNhanThanhToanVM GetByName(string name)
        {
            var tk = _db.TaiKhoanNhanThanhToans.SingleOrDefault(m => m.STKNhan == name);
            if (tk != null)
            {
                return new TaiKhoanNhanThanhToanVM
                {
                    IdTK = tk.IdTK,
                    TenTKNhan = tk.TenTKNhan,
                    STKNhan = tk.STKNhan,
                    NganHang = tk.NganHang,
                    ChiNhanh = tk.ChiNhanh
                };
            }
            return null;
        }

        public void Update(TaiKhoanNhanThanhToanVM vm)
        {
            var tk = _db.TaiKhoanNhanThanhToans.SingleOrDefault(m => m.IdTK == vm.IdTK);
            if (tk != null)
            {
                tk.TenTKNhan = vm.TenTKNhan;
                tk.STKNhan = vm.STKNhan;
                tk.NganHang = vm.NganHang;
                tk.ChiNhanh = vm.ChiNhanh;
                _db.SaveChanges();
            }
        }
    }
}
