using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace quraan_castle_api.Models
{
    public partial class quraancastledbContext : DbContext
    {
        public quraancastledbContext()
        {
        }

        public quraancastledbContext(DbContextOptions<quraancastledbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Adminlogin> Adminlogins { get; set; } = null!;
        public virtual DbSet<Attend> Attends { get; set; } = null!;
        public virtual DbSet<Mentor> Mentors { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<Rate> Rates { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Trialsession> Trialsessions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userlogin> Userlogins { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=quraancastledb;user id=root;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"), x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.HasIndex(e => e.Email, "Email_Index");

                entity.HasIndex(e => e.Password, "Password_Index");

                entity.HasIndex(e => e.Uuid, "UUID_INEX")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Uuid)
                    .HasMaxLength(200)
                    .HasColumnName("UUID");
            });

            modelBuilder.Entity<Adminlogin>(entity =>
            {
                entity.ToTable("adminlogins");

                entity.HasIndex(e => e.AdminId, "ADMIN_FK");

                entity.HasIndex(e => e.Token, "Token_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.EndedAt).HasMaxLength(6);

                entity.Property(e => e.Token).HasMaxLength(200);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Adminlogins)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ADMIN_FK");
            });

            modelBuilder.Entity<Attend>(entity =>
            {
                entity.ToTable("attends");

                entity.HasIndex(e => e.CustomerId, "attend_customer_idx");

                entity.HasIndex(e => e.SectionId, "attend_section_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Attends)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attend_customer");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Attends)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attend_section");
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.ToTable("mentors");

                entity.Property(e => e.Country).HasMaxLength(45);

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Gender).HasComment("0- women\\n1- man");

                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Nationality).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasIndex(e => e.CustomerId, "message_customer_idx");

                entity.Property(e => e.Message1)
                    .HasColumnType("text")
                    .HasColumnName("Message");

                entity.Property(e => e.Subject).HasMaxLength(45);

                entity.Property(e => e.TypeId).HasComment("0- message. \n1- request. \n2- account issue.\n3- technical issue.");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_customer");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plan");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Currency)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("'USD'");

                entity.Property(e => e.Desc).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("rates");

                entity.HasIndex(e => e.UserId, "rate_customer_idx");

                entity.HasIndex(e => e.TeacherId, "rate_teacher_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Rate1)
                    .HasColumnName("Rate")
                    .HasComment("from 0 : 5 ");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rate_teacher");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rate_user");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");

                entity.HasIndex(e => e.UserId, "review_customer_idx");

                entity.HasIndex(e => e.TeacherId, "review_techer_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_teacher");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_user");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("sections");

                entity.HasIndex(e => e.TeacherId, "section_techer_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Desc).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Subject).HasMaxLength(45);

                entity.Property(e => e.Time).HasColumnType("time");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_teacher");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.HasIndex(e => e.StatusId, "StatusId");

                entity.HasIndex(e => e.PlanId, "Subcription_Plan_idx");

                entity.HasIndex(e => e.CustomerId, "subcriptio_customer_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Currency)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("'USD'");

                entity.Property(e => e.Error).HasColumnType("text");

                entity.Property(e => e.FromDate).HasMaxLength(6);

                entity.Property(e => e.ToAccount).HasMaxLength(100);

                entity.Property(e => e.ToDate).HasMaxLength(6);

                entity.Property(e => e.Trn)
                    .HasMaxLength(100)
                    .HasColumnName("TRN");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subcriptio_customer");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("Subcription_Plan");
            });

            modelBuilder.Entity<Trialsession>(entity =>
            {
                entity.ToTable("trialsession");

                entity.HasIndex(e => e.CustomerId, "trialsession_customer_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.StatusId)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'0'")
                    .HasComment("0 - pending\\n1 - schedulaed");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Trialsessions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("trialsession_customer");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Uuid, "UUID_Index");

                entity.HasIndex(e => e.Email, "email_index")
                    .IsUnique();

                entity.HasIndex(e => e.Password, "password_index");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Gender)
                    .HasColumnType("bit(1)")
                    .HasComment("0 women\n1 man");

                entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Uuid).HasColumnName("UUID");
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.ToTable("userlogins");

                entity.HasIndex(e => e.UserId, "User_Login_FK_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.EndedAt).HasMaxLength(6);

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("User_Login_FK");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("videos");

                entity.HasIndex(e => e.Name, "name");

                entity.HasIndex(e => e.TeacherId, "video_teacher_idx");

                entity.Property(e => e.CreatedAt).HasMaxLength(6);

                entity.Property(e => e.Desc).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("video_teacher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
