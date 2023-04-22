using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.CtyXuatHoaDon;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.CtyXuatHoaDonServices
{
    public class CtyXuatHoaDonServices : ICtyXuatHoaDonServices
    {
        private readonly DB _db;
        public CtyXuatHoaDonServices(DB db)
        {
            _db = db;
        }

        public CtyXuatHoaDonVM Add(CtyXuatHoaDonModel model)
        {
            var cty = new CtyXuatHoaDon
            {
                TenCty = model.TenCty,
                DiaChi = model.DiaChi,
                SDT = model.SDT
            };
            _db.Add(cty);
            _db.SaveChanges();
            return new CtyXuatHoaDonVM
            {
                MaCty = cty.MaCty,
                TenCty = cty.TenCty,
                DiaChi = cty.DiaChi,
                SDT = cty.SDT
            };
        }

        public void DeleteById(int id)
        {
            var cty = _db.CtyXuatHoaDons.SingleOrDefault(n => n.MaCty == id);
            if (cty != null)
            {
                _db.Remove(cty);
                _db.SaveChanges();
            }
        }

        public List<CtyXuatHoaDonVM> GetAll()
        {
            var data = _db.CtyXuatHoaDons.Select(n => new CtyXuatHoaDonVM
            {
                MaCty = n.MaCty,
                TenCty = n.TenCty,
                DiaChi = n.DiaChi,
                SDT = n.SDT
            });
            return data.ToList();
        }

        public CtyXuatHoaDonVM GetById(int id)
        {
            var cty = _db.CtyXuatHoaDons.SingleOrDefault(n => n.MaCty == id);
            if (cty != null)
            {
                return new CtyXuatHoaDonVM
                {
                    MaCty = cty.MaCty,
                    TenCty = cty.TenCty,
                    DiaChi = cty.DiaChi,
                    SDT = cty.SDT
                };
            }
            return null;
        }

        public CtyXuatHoaDonVM GetByName(string name)
        {
            var cty = _db.CtyXuatHoaDons.SingleOrDefault(n => n.TenCty == name);
            if (cty != null)
            {
                return new CtyXuatHoaDonVM
                {
                    MaCty = cty.MaCty,
                    TenCty = cty.TenCty,
                    DiaChi = cty.DiaChi,
                    SDT = cty.SDT
                };
            }
            return null;
        }

        public void Update(CtyXuatHoaDonVM vm)
        {
            var cty = _db.CtyXuatHoaDons.SingleOrDefault(n => n.MaCty == vm.MaCty);
            if (cty != null)
            {
                cty.TenCty = vm.TenCty;
                cty.DiaChi = vm.DiaChi;
                cty.SDT = vm.SDT;
                _db.SaveChanges();
            }
        }
    }
}
