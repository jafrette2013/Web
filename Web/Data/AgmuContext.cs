using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data;

public partial class AgmuContext : DbContext
{
    public AgmuContext()
    {
    }

    public AgmuContext(DbContextOptions<AgmuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicProgram> AcademicPrograms { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DPKERKO7\\SQLEXPRESS;Initial Catalog=AGMU;TrustServerCertificate=True; Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Programs");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Courses_Classes");

            entity.HasOne(d => d.Staff).WithMany(p => p.Classes)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Classes_Staffs");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.Program).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_Courses_Programs");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.AcademicProgram).WithMany(p => p.Students)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_Students_Programs");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasIndex(e => new { e.ClassId, e.StudentId }, "IX_StudentClasses").IsUnique();

            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClasses_Classes");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentClasses_Students");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
