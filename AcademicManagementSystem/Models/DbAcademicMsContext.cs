using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagementSystem.Models;

public partial class DbAcademicMsContext : DbContext
{
    public DbAcademicMsContext()
    {
    }

    public DbAcademicMsContext(DbContextOptions<DbAcademicMsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblLesson> TblLessons { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblTeacher> TblTeachers { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=dbAcademicMS;uid=sa; pwd=as; trustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblLesson>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("tblLessons");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Course).HasMaxLength(50);
            entity.Property(e => e.Faculty).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.TeacherId)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("TeacherID");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("tblStudents");

            entity.Property(e => e.StudentId)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("StudentID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTeacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId);

            entity.ToTable("tblTeachers");

            entity.Property(e => e.TeacherId)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("TeacherID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("tblUsers");

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Authority)
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
