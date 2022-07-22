using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace LMS_Project.Models
{
    public partial class LMSContext : DbContext
    {
        public LMSContext()
        {
        }

        public LMSContext(DbContextOptions<LMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }
        public virtual DbSet<BorrowDetail> BorrowDetails { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("Mydatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AutId);

                entity.ToTable("Author");

                entity.Property(e => e.AutId).HasColumnName("aut_id");

                entity.Property(e => e.AutAddress)
                    .HasMaxLength(50)
                    .HasColumnName("aut_address");

                entity.Property(e => e.AutDob)
                    .HasColumnType("datetime")
                    .HasColumnName("aut_dob");

                entity.Property(e => e.AutGender).HasColumnName("aut_gender");

                entity.Property(e => e.AutName)
                    .HasMaxLength(50)
                    .HasColumnName("aut_name");

                entity.Property(e => e.AutPhone)
                    .HasMaxLength(50)
                    .HasColumnName("aut_phone");

                entity.Property(e => e.AutStatus).HasColumnName("aut_status");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK_Author_User");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BId);

                entity.ToTable("Book");

                entity.Property(e => e.BId).HasColumnName("b_id");

                entity.Property(e => e.AutId).HasColumnName("aut_id");

                entity.Property(e => e.BCateId)
                    .HasMaxLength(50)
                    .HasColumnName("b_cate_id");

                entity.Property(e => e.BDesc).HasColumnName("b_desc");

                entity.Property(e => e.BImg).HasColumnName("b_img");

                entity.Property(e => e.BLastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("b_lastupdated");

                entity.Property(e => e.BName)
                    .HasMaxLength(50)
                    .HasColumnName("b_name");

                entity.Property(e => e.BNumBorrow).HasColumnName("b_num_borrow");

                entity.Property(e => e.BPrice)
                    .HasColumnType("money")
                    .HasColumnName("b_price");

                entity.Property(e => e.BStatus).HasColumnName("b_status");

                entity.Property(e => e.BStock).HasColumnName("b_stock");

                entity.HasOne(d => d.Aut)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AutId)
                    .HasConstraintName("FK_Book_Author");

                entity.HasOne(d => d.BCate)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BCateId)
                    .HasConstraintName("FK_Book_Book_Category");
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => e.BCateId);

                entity.ToTable("Book_Category");

                entity.Property(e => e.BCateId)
                    .HasMaxLength(50)
                    .HasColumnName("b_cate_id");

                entity.Property(e => e.BCateDes)
                    .IsRequired()
                    .HasColumnName("b_cate_des");

                entity.Property(e => e.BCateImg)
                    .IsRequired()
                    .HasColumnName("b_cate_img");

                entity.Property(e => e.BCateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("b_cate_name");
            });

            modelBuilder.Entity<Borrow>(entity =>
            {
                entity.HasKey(e => e.BrId);

                entity.ToTable("Borrow");

                entity.Property(e => e.BrId).HasColumnName("br_id");

                entity.Property(e => e.BrDate)
                    .HasColumnType("datetime")
                    .HasColumnName("br_date");

                entity.Property(e => e.BrResend)
                    .HasColumnType("datetime")
                    .HasColumnName("br_resend");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK_Borrow_User");
            });

            modelBuilder.Entity<BorrowDetail>(entity =>
            {
                entity.HasKey(e => new { e.BId, e.BrId });

                entity.ToTable("Borrow_Detail");

                entity.Property(e => e.BId).HasColumnName("b_id");

                entity.Property(e => e.BrId).HasColumnName("br_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.BorrowDetails)
                    .HasForeignKey(d => d.BId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Borrow_Detail_Book");

                entity.HasOne(d => d.Br)
                    .WithMany(p => p.BorrowDetails)
                    .HasForeignKey(d => d.BrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Borrow_Detail_Borrow");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RId);

                entity.ToTable("Role");

                entity.Property(e => e.RId).HasColumnName("r_id");

                entity.Property(e => e.RDesc)
                    .IsRequired()
                    .HasColumnName("r_desc");

                entity.Property(e => e.RName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("r_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.ToTable("User");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.RId).HasColumnName("r_id");

                entity.Property(e => e.UAddress).HasColumnName("u_address");

                entity.Property(e => e.UDob)
                    .HasColumnType("datetime")
                    .HasColumnName("u_dob");

                entity.Property(e => e.UEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("u_email");

                entity.Property(e => e.UGender).HasColumnName("u_gender");

                entity.Property(e => e.UPassword)
                    .IsRequired()
                    .HasColumnName("u_password");

                entity.Property(e => e.UPhone)
                    .HasMaxLength(50)
                    .HasColumnName("u_phone");

                entity.Property(e => e.UStatus).HasColumnName("u_status");

                entity.Property(e => e.UUsername)
                    .HasMaxLength(50)
                    .HasColumnName("u_username");

                entity.Property(e => e.UWallet)
                    .HasMaxLength(50)
                    .HasColumnName("u_wallet");

                entity.HasOne(d => d.RIdNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
