using QuanLyBanHangAPI.Models.ChuyenMucBlog;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.ChuyenMucBlogServices
{
    public interface IChuyenMucBlogServices
    {
        List<ChuyenMucBlogVM> GetAll();
        ChuyenMucBlogVM ChuyenMucBlogVMGetById(int id);
        ChuyenMucBlogVM ChuyenMucBlogVMGetByName(string name);
        ChuyenMucBlogVM Add(ChuyenMucBlogModel model);
        void Delete(int id);
        string Update(ChuyenMucBlogVM vm);
    }
}
