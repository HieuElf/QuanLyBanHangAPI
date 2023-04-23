using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.GoiDichVu;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.GoiDIchVuServices
{
    public class GoiDichVuServices : IGoiDichVuServices
    {
        private readonly DB _db;
        public GoiDichVuServices(DB db)
        {
            _db = db;
        }

        public GoiDichVuVM Add(GoiDichVuModel model)
        {
            var goi = new GoiDichVu
            {
                TenGoi = model.TenGoi,
                MaNhaCungCap = model.MaNhaCungCap
            };
            _db.Add(goi);
            _db.SaveChanges();
            return new GoiDichVuVM
            {
                MaGoi = goi.MaGoi,
                TenGoi = goi.TenGoi,
                MaCungCap = goi.MaNhaCungCap             
            };
        }

        public void Delete(int id)
        {
            var goi = _db.GoiDichVus.SingleOrDefault(n => n.MaGoi == id);
            if (goi != null)
            {
                _db.Remove(goi);
                _db.SaveChanges();
            }
        }

        public List<GoiDichVuVM> GetAll()
        {
            var gois = _db.GoiDichVus.Select(n => new GoiDichVuVM
            {
                MaGoi = n.MaGoi,
                TenGoi = n.TenGoi,
                MaCungCap = n.MaNhaCungCap
            });
            return gois.ToList();
        }

        public GoiDichVuVM GetById(int id)
        {
            var goi = _db.GoiDichVus.SingleOrDefault(n => n.MaGoi == id);
            if(goi != null)
            {
                return new GoiDichVuVM
                {
                    MaGoi = goi.MaGoi,
                    TenGoi = goi.TenGoi,
                    MaCungCap = goi.MaNhaCungCap
                };
            }
            return null;
        }

        public GoiDichVuVM GetByName(string name)
        {
            var goi = _db.GoiDichVus.SingleOrDefault(n => n.TenGoi == name);
            if (goi != null)
            {
                return new GoiDichVuVM
                {
                    MaGoi = goi.MaGoi,
                    TenGoi = goi.TenGoi,
                    MaCungCap = goi.MaNhaCungCap
                };
            }
            return null;
        }

        public void Update(GoiDichVuVM vm)
        {
            var goi = _db.GoiDichVus.SingleOrDefault(n => n.MaGoi == vm.MaGoi);
            if (goi != null)
            {
                goi.TenGoi = vm.TenGoi;
                goi.MaNhaCungCap = vm.MaCungCap;
                _db.SaveChanges();
            }
        }
    }
}
