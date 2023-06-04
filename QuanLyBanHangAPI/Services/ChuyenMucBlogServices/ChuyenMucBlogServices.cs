using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.ChuyenMucBlog;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.ChuyenMucBlogServices
{
    public class ChuyenMucBlogServices : IChuyenMucBlogServices
    {
        private readonly DB _db;
        public ChuyenMucBlogServices(DB db)
        {
            _db = db;
        }
        public ChuyenMucBlogVM Add(ChuyenMucBlogModel model)
        {
            var cm = new ChuyenMucBlog
            {
                tenChuyenMuc = model.tenChuyenMuc,
                moTa = model.moTa
            };
            _db.Add(cm);
            _db.SaveChanges();
            return new ChuyenMucBlogVM
            {
                maChuyenMuc = cm.maChuyenMuc,
                tenChuyenMuc = cm.tenChuyenMuc,
                moTa = cm.moTa
            };
        }

        public ChuyenMucBlogVM ChuyenMucBlogVMGetById(int id)
        {
            var cm = _db.ChuyenMucBlogs.SingleOrDefault(m => m.maChuyenMuc == id);
            if (cm != null)
            {
                return new ChuyenMucBlogVM
                {
                    maChuyenMuc = cm.maChuyenMuc,
                    tenChuyenMuc = cm.tenChuyenMuc,
                    moTa = cm.moTa
                };
            }
            return null;
        }

        public ChuyenMucBlogVM ChuyenMucBlogVMGetByName(string name)
        {
            var cm = _db.ChuyenMucBlogs.SingleOrDefault(m => m.tenChuyenMuc == name);
            if (cm != null)
            {
                return new ChuyenMucBlogVM
                {
                    maChuyenMuc = cm.maChuyenMuc,
                    tenChuyenMuc = cm.tenChuyenMuc,
                    moTa = cm.moTa
                };
            }
            return null;
        }

        public void Delete(int id)
        {
            var cm = _db.ChuyenMucBlogs.SingleOrDefault(m => m.maChuyenMuc == id);
            if (cm != null)
            {
                _db.Remove(cm);
                _db.SaveChanges();
            }
        }

        public List<ChuyenMucBlogVM> GetAll()
        {
            var cms = _db.ChuyenMucBlogs.Select(m => new ChuyenMucBlogVM
            {
                maChuyenMuc = m.maChuyenMuc,
                tenChuyenMuc = m.tenChuyenMuc,
                moTa = m.moTa
            });
            return cms.ToList();
        }

        public string Update(ChuyenMucBlogVM vm)
        {
            var cm = _db.ChuyenMucBlogs.SingleOrDefault(m => m.maChuyenMuc == vm.maChuyenMuc);
            if (cm != null)
            {
                var duplicate = _db.ChuyenMucBlogs
                    .Where(m => m.tenChuyenMuc == vm.tenChuyenMuc && m.maChuyenMuc != vm.maChuyenMuc)
                    .ToList();
                if (duplicate.Any())
                {
                    return "Đã tồn tại dữ liệu khác trùng tên";
                }
                cm.tenChuyenMuc = vm.tenChuyenMuc;
                cm.moTa = vm.moTa;
                _db.Update(cm);
                _db.SaveChanges();
                return "OK";
            }
            return "Không tồn tại";
        }
    }
}
