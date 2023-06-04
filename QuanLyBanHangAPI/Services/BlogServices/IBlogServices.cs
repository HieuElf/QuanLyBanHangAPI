using QuanLyBanHangAPI.Models.Blog;
using System.Collections.Generic;

namespace QuanLyBanHangAPI.Services.BlogServices
{
    public interface IBlogServices
    {
        List<BlogVM> GetAll();
        BlogVM GetByID(int id);
        BlogVM Add(BlogModel model);
        string Update(BlogVM vm);
        bool Delete(int id);
    }
}
