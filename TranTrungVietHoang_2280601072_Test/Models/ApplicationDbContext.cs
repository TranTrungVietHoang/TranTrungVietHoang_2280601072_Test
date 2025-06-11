
using Microsoft.EntityFrameworkCore;

namespace TranTrungVietHoang_2280601072_Test.Models

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NganhHoc> NganhHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key for ChiTietDangKy
            modelBuilder.Entity<ChiTietDangKy>()
                .HasKey(c => new { c.MaDK, c.MaHP });

            // Configure relationships
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.NganhHoc)
                .WithMany(n => n.SinhViens)
                .HasForeignKey(s => s.MaNganh);

            modelBuilder.Entity<DangKy>()
                .HasOne(d => d.SinhVien)
                .WithMany(s => s.DangKys)
                .HasForeignKey(d => d.MaSV);

            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.DangKy)
                .WithMany(d => d.ChiTietDangKys)
                .HasForeignKey(c => c.MaDK);

            modelBuilder.Entity<ChiTietDangKy>()
                .HasOne(c => c.HocPhan)
                .WithMany(h => h.ChiTietDangKys)
                .HasForeignKey(c => c.MaHP);
        }
    }
}
