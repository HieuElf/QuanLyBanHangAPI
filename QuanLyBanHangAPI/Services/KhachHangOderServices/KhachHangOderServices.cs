using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.KhachHangOder;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace QuanLyBanHangAPI.Services.KhachHangOderServices
{
    public class KhachHangOderServices : IKhachHangOderServices
    {
        private readonly DB _db;
        public KhachHangOderServices(DB db)
        {
            _db = db;
        }
        public KhachHangOderVM Add(KhachHangOderModel model)
        {
            var kh = new KhachHangOder
            {
                MaSoThue = model.MaSoThue,
                TenCongTy = model.TenCongTy,
                TenNguoiLienHe = model.TenNguoiLienHe,
                SoDienThoai = model.SoDienThoai,
                MaNhaCungCap = model.MaNhaCungCap,
                MaGoi = model.MaGoi
            };
            _db.Add(kh);
            _db.SaveChanges();
            return new KhachHangOderVM
            {
                Id = kh.Id,
                MaSoThue = kh.MaSoThue,
                TenCongTy = kh.TenCongTy,
                TenNguoiLienHe = kh.TenNguoiLienHe,
                SoDienThoai = kh.SoDienThoai,
                MaNhaCungCap =kh.MaNhaCungCap,
                MaGoi =kh.MaGoi
            };
        }

        public void Delete(int id)
        {
            var kh = _db.KhachHangOders.SingleOrDefault(m => m.Id == id);
            if (kh != null)
            {
                _db.Remove(kh);
                _db.SaveChanges();
            }
        }

        public List<KhachHangOderVM> GetAll()
        {
            var khs = _db.KhachHangOders.Select(n => new KhachHangOderVM
            {
                Id = n.Id,
                MaSoThue = n.MaSoThue,
                TenCongTy = n.TenCongTy,
                TenNguoiLienHe = n.TenNguoiLienHe,
                SoDienThoai = n.SoDienThoai,
                MaNhaCungCap = n.MaNhaCungCap,
                MaGoi = n.MaGoi
            });
            return khs.ToList();
        }

        public KhachHangOderVM GetById(int id)
        {
            var kh = _db.KhachHangOders.SingleOrDefault(n => n.Id == id);
            if (kh != null)
            {
                return new KhachHangOderVM
                {
                    Id = kh.Id,
                    MaSoThue =kh.MaSoThue,
                    TenCongTy = kh.TenCongTy,
                    TenNguoiLienHe = kh.TenNguoiLienHe,
                    SoDienThoai = kh.SoDienThoai,
                    MaNhaCungCap = kh.MaNhaCungCap,
                    MaGoi =kh.MaGoi
                };
            }
            return null;
        }

        public KhachHangOderVM GetByMST(string mst)
        {
            var oder = _db.KhachHangOders.SingleOrDefault(m => m.MaSoThue == mst);
            if (oder != null)
            {
                return new KhachHangOderVM
                {
                    Id = oder.Id,
                    MaSoThue = oder.MaSoThue,
                    TenCongTy = oder.TenCongTy,
                    TenNguoiLienHe = oder.TenNguoiLienHe,
                    SoDienThoai = oder.SoDienThoai,
                    MaNhaCungCap = oder.MaNhaCungCap,
                    MaGoi = oder.MaGoi
                };
            }
            return null;
        }

        public void Update(KhachHangOderVM vm)
        {
            var kh = _db.KhachHangOders.SingleOrDefault(n => n.Id == vm.Id);
            if (kh != null)
            {
                kh.MaSoThue = vm.MaSoThue;
                kh.TenCongTy = vm.TenCongTy;
                kh.TenNguoiLienHe = vm.TenNguoiLienHe;
                kh.SoDienThoai = vm.SoDienThoai;
                kh.MaNhaCungCap = vm.MaNhaCungCap;
                kh.MaGoi = vm.MaGoi;
                _db.SaveChanges();
            }
        }
    }
}
