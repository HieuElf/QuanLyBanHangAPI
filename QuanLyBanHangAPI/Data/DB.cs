using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHangAPI.Data
{
    public class DB : IdentityDbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }
        #region DbSet
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
