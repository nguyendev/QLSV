using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLSV.Models
{
    public partial class QLSVContext : DbContext
    {
        public QLSVContext(DbContextOptions<QLSVContext> options)
    : base(options)
{ }
        public virtual DbSet<DangKy> DangKy { get; set; }
        public virtual DbSet<HocPhan> HocPhan { get; set; }
        public virtual DbSet<SinhVien> SinhVien { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DangKy>(entity =>
            {
                entity.HasKey(e => new { e.Mahp, e.Mssv })
                    .HasName("PK_SinhVien");

                entity.Property(e => e.Mahp)
                    .HasColumnName("MAHP")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Mssv)
                    .HasColumnName("MSSV")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.MahpNavigation)
                    .WithMany(p => p.DangKy)
                    .HasForeignKey(d => d.Mahp)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__DangKy__MAHP__145C0A3F");

                entity.HasOne(d => d.MssvNavigation)
                    .WithMany(p => p.DangKy)
                    .HasForeignKey(d => d.Mssv)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__DangKy__MSSV__286302EC");
            });

            modelBuilder.Entity<HocPhan>(entity =>
            {
                entity.HasKey(e => e.Mahp)
                    .HasName("PK__HocPhan__603F20DA6DE4D4CB");

                entity.Property(e => e.Mahp)
                    .HasColumnName("MAHP")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Ngaybd)
                    .HasColumnName("NGAYBD")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Ngaykt)
                    .HasColumnName("NGAYKT")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Phonghoc)
                    .HasColumnName("PHONGHOC")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.Siso).HasColumnName("SISO");

                entity.Property(e => e.Tenmon)
                    .HasColumnName("TENMON")
                    .HasMaxLength(30);

                entity.Property(e => e.Thu)
                    .HasColumnName("THU")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Tiethoc)
                    .HasColumnName("TIETHOC")
                    .HasColumnType("varchar(5)");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.Mssv)
                    .HasName("PK__SinhVien__6CB3B7F9F0CC0B07");

                entity.Property(e => e.Mssv)
                    .HasColumnName("MSSV")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Diachi)
                    .HasColumnName("DIACHI")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Gioitinh)
                    .HasColumnName("GIOITINH")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");

                entity.Property(e => e.Hoten)
                    .HasColumnName("HOTEN")
                    .HasMaxLength(30);

                entity.Property(e => e.Lop)
                    .HasColumnName("LOP")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Namsinh)
                    .HasColumnName("NAMSINH")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Nganh)
                    .HasColumnName("NGANH")
                    .HasMaxLength(20);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasColumnType("varchar(11)");
            });
        }
    }
}