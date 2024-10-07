using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShowPic.Entity
{
    public partial class picturestoreContext : DbContext
    {
        public picturestoreContext()
        {
        }

        public picturestoreContext(DbContextOptions<picturestoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pictureeditor> Pictureeditors { get; set; } = null!;
        public virtual DbSet<Pictureentity> Pictureentities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=picturestore;charset=utf8;uid=root;pwd=123456;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Pictureeditor>(entity =>
            {
                entity.ToTable("pictureeditor");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Role).HasColumnType("enum('Admin','User')");
            });

            modelBuilder.Entity<Pictureentity>(entity =>
            {
                entity.HasKey(e => e.PicId)
                    .HasName("PRIMARY");

                entity.ToTable("pictureentity");

                entity.Property(e => e.PicDescription).HasMaxLength(256);

                entity.Property(e => e.PicName).HasMaxLength(256);

                entity.Property(e => e.PicTag).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
