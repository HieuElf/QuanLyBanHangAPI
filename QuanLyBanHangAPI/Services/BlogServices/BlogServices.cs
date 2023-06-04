using QuanLyBanHangAPI.Data;
using QuanLyBanHangAPI.Models.Blog;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyBanHangAPI.Services.BlogServices
{
    public class BlogServices : IBlogServices
    {
        private readonly DB _db;
        public BlogServices(DB db)
        {
            _db = db;
        }
        public BlogVM Add(BlogModel model)
        {
            var blog = new Blog
            {
                tenBaiViet = model.tenBaiViet,
                maChuyenMuc = model.maChuyenMuc,
                tomTat = model.tomTat,
                noiDung = model.noiDung,
                ngayBaiViet = model.ngayBaiViet,
                ngayChinhSuaCuoi = model.ngayChinhSuaCuoi,
                trangThai = model.trangThai,
                luotXem = 0,
                anhBaiViet= model.anhBaiViet,
                anhBia = model.anhBia
            };

            _db.Add(blog);
            _db.SaveChanges();
            return new BlogVM
            {
                maBaiViet = blog.maBaiViet,
                tenBaiViet = blog.tenBaiViet,
                maChuyenMuc = (int)blog.maChuyenMuc,
                tomTat = blog.tomTat,
                noiDung = blog.noiDung,
                anhBaiViet = blog.anhBaiViet,
                anhBia= blog.anhBia,
                trangThai = blog.trangThai,
                luotXem = 0,
                ngayBaiViet = blog.ngayBaiViet,
                ngayChinhSuaCuoi = blog.ngayChinhSuaCuoi
            };
        }

        public bool Delete(int id)
        {
            var baiviet = _db.Blogs.SingleOrDefault(p => p.maBaiViet == id);
            if (baiviet != null)
            {
                _db.Remove(baiviet);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<BlogVM> GetAll()
        {
            var listBaiViet = _db.Blogs.Select(p => new BlogVM
            {
                maBaiViet = p.maBaiViet,
                maChuyenMuc = (int)p.maChuyenMuc,
                tenBaiViet = p.tenBaiViet,
                tomTat = p.tomTat,
                anhBia = p.anhBia,
                noiDung = p.noiDung,
                anhBaiViet = p.anhBaiViet,
                ngayBaiViet = p.ngayBaiViet,
                ngayChinhSuaCuoi = p.ngayChinhSuaCuoi,
                luotXem = p.luotXem,
                trangThai = p.trangThai
            });
            return listBaiViet.ToList();
        }

        public BlogVM GetByID(int id)
        {
            var baiviet = _db.Blogs.SingleOrDefault(p => p.maBaiViet== id);
            if (baiviet != null)
            {
                baiviet.luotXem++;
                _db.Update(baiviet);
                _db.SaveChanges();
                return new BlogVM
                {
                    maBaiViet = baiviet.maBaiViet,
                    maChuyenMuc = (int)baiviet.maChuyenMuc,
                    tenBaiViet = baiviet.tenBaiViet,
                    tomTat = baiviet.tomTat,
                    anhBia = baiviet.anhBia,
                    noiDung = baiviet.noiDung,
                    anhBaiViet = baiviet.anhBaiViet,
                    ngayBaiViet = baiviet.ngayBaiViet,
                    ngayChinhSuaCuoi = baiviet.ngayChinhSuaCuoi,
                    luotXem = baiviet.luotXem,
                    trangThai = baiviet.trangThai
                };
            }
            return null;
        }

        public string Update(BlogVM vm)
        {
            var blog = _db.Blogs.SingleOrDefault(p => p.maBaiViet== vm.maBaiViet);
            if (blog != null)
            {
                var duplicate = _db.Blogs
                    .Where(m => m.tenBaiViet == vm.tenBaiViet && m.maBaiViet != vm.maBaiViet)
                    .ToList();
                if (duplicate.Any())
                {
                    return "Đã tồn tại dữ liệu khác trùng tên";
                }
                blog.tenBaiViet = vm.tenBaiViet;
                blog.anhBaiViet = vm.anhBaiViet;
                blog.tomTat = vm.tomTat;
                blog.noiDung = vm.noiDung;
                blog.ngayBaiViet = vm.ngayBaiViet;
                blog.ngayChinhSuaCuoi = vm.ngayChinhSuaCuoi;
                blog.anhBia = vm.anhBia;
                blog.trangThai = vm.trangThai;
                blog.luotXem = vm.luotXem;
                blog.maChuyenMuc = vm.maChuyenMuc;
                _db.Update(blog);
                _db.SaveChanges();
                return "OK";
            }
            return "Không tồn tại";
        }
    }
}
