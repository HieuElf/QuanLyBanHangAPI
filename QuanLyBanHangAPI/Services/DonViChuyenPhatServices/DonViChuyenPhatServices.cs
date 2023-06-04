using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.DonViChuyenPhat;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.DonViChuyenPhatServices
{
    public class DonViChuyenPhatServices : IDonViChuyenPhatServices
    {
        private readonly DB _db;
        public DonViChuyenPhatServices(DB db)
        {
            _db = db;
        }
        public DonViChuyenPhatVM Add(DonViChuyenPhatModel model)
        {
            var dv = new DonViChuyenPhat
            {
                TenDonVi = model.TenDonVi,
                SDT = model.SDT,
                GhiChu = model.GhiChu
            };
            _db.Add(dv);
            _db.SaveChanges();
            return new DonViChuyenPhatVM
            {
                MaDonViChuyenPhat = dv.MaDonViChuyenPhat,
                TenDonVi = dv.TenDonVi,
                SDT = dv.SDT,
                GhiChu = dv.GhiChu
            };
        }

        public void Delete(int id)
        {
            var data = _db.DonViChuyenPhats.SingleOrDefault(m => m.MaDonViChuyenPhat == id);
            if (data != null)
            {
                _db.Remove(data);
                _db.SaveChanges();
            }
        }

        public List<DonViChuyenPhatVM> GetAll()
        {
            var datas = _db.DonViChuyenPhats.Select(m => new DonViChuyenPhatVM
            {
                MaDonViChuyenPhat = m.MaDonViChuyenPhat,
                TenDonVi = m.TenDonVi,
                SDT = m.SDT,
                GhiChu = m.GhiChu
            });
            return datas.ToList();
        }

        public DonViChuyenPhatVM GetByID(int id)
        {
            var data = _db.DonViChuyenPhats.SingleOrDefault(m => m.MaDonViChuyenPhat == id);
            if (data != null)
            {
                return new DonViChuyenPhatVM
                {
                    MaDonViChuyenPhat = data.MaDonViChuyenPhat,
                    TenDonVi = data.TenDonVi,
                    SDT = data.SDT,
                    GhiChu = data.GhiChu
                };
            }
            return null;
        }

        public DonViChuyenPhatVM GetByName(string name)
        {
            var data = _db.DonViChuyenPhats.SingleOrDefault(m => m.TenDonVi == name);
            if (data != null)
            {
                return new DonViChuyenPhatVM
                {
                    MaDonViChuyenPhat = data.MaDonViChuyenPhat,
                    TenDonVi = data.TenDonVi,
                    SDT = data.SDT,
                    GhiChu = data.GhiChu
                };
            }
            return null;
        }

        public string Update(DonViChuyenPhatVM vm)
        {
            var data = _db.DonViChuyenPhats.SingleOrDefault(m => m.MaDonViChuyenPhat == vm.MaDonViChuyenPhat);
            if (data != null)
            {
                var duplicate = _db.DonViChuyenPhats
                    .Where(m => m.TenDonVi == vm.TenDonVi && m.MaDonViChuyenPhat != vm.MaDonViChuyenPhat)
                    .ToList();
                if (duplicate.Any())
                {
                    return "Đã tồn tại dữ liệu khác trùng tên";
                }
                data.TenDonVi = vm.TenDonVi;
                data.SDT = vm.SDT;
                data.GhiChu = vm.GhiChu;
                _db.SaveChanges();
                return "OK";
            }
            return "Không tồn tại";
        }
    }
}
