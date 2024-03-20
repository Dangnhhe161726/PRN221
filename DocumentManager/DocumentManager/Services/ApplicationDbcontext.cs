using DocumentManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace DocumentManager.Services
{
    public class ApplicationDbcontext : IdentityDbContext<Users>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options) { }
        public virtual DbSet<AgencyIssue> AgencyIssues { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Emergency> Emergencies { get; set; } = null!;
        public virtual DbSet<GroupDocument> GroupDocuments { get; set; } = null!;
        public virtual DbSet<NameDocument> NameDocuments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Sercurity> Sercurities { get; set; } = null!;
        public virtual DbSet<Signer> Signers { get; set; } = null!;
        public virtual DbSet<Specialize> Specializes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";
            modelBuilder.Entity<IdentityRole>().HasData(admin, employee);


            modelBuilder.Entity<AgencyIssue>(entity =>
            {
                entity.ToTable("Agency_Issues");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.AgencyIssuesId).HasColumnName("Agency_Issues_id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_id");

                entity.Property(e => e.DateOut)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_out");

                entity.Property(e => e.DateSign)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Sign");

                entity.Property(e => e.DateTo)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_To");

                entity.Property(e => e.Describe).HasMaxLength(255);
                entity.Property(e => e.Aid)
                                .HasColumnType("nvarchar(450)")
                                .HasMaxLength(450);


                entity.Property(e => e.EmergencyId).HasColumnName("Emergency_id");

                entity.Property(e => e.GroupDocumentId).HasColumnName("Group_DocumentId");

                entity.Property(e => e.NameDocumentId).HasColumnName("Name_DocumentId");

                entity.Property(e => e.Receiver).HasMaxLength(255);

                entity.Property(e => e.SecurityId).HasColumnName("Security_id");

                entity.Property(e => e.SignerId).HasColumnName("Signer_id");

                entity.Property(e => e.SpecializedId).HasColumnName("Specialized_id");

                entity.Property(e => e.Symbol).HasMaxLength(255);

                entity.Property(e => e.Aid).HasMaxLength(255);

                entity.HasOne(d => d.AgencyIssues)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.AgencyIssuesId)
                    .HasConstraintName("FK_Documents_Agency_Issues");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Documents_Categories");

                entity.HasOne(d => d.Emergency)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.EmergencyId)
                    .HasConstraintName("FK_Documents_Emergencies");

                entity.HasOne(d => d.GroupDocument)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.GroupDocumentId)
                    .HasConstraintName("FK_Documents_Group_Document");

                entity.HasOne(d => d.NameDocument)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.NameDocumentId)
                    .HasConstraintName("FK_Documents_Name_Document");

                entity.HasOne(d => d.Security)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.SecurityId)
                    .HasConstraintName("FK_Documents_Sercurities");

                entity.HasOne(d => d.Signer)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.SignerId)
                    .HasConstraintName("FK_Documents_Signer");

                entity.HasOne(d => d.Specialized)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.SpecializedId)
                    .HasConstraintName("FK_Documents_Specialize");

                entity.HasOne(d => d.Users)
                   .WithMany(p => p.Documents)
                   .HasForeignKey(d => d.Aid)
                   .HasConstraintName("FK_Documents_Users");
            });

            modelBuilder.Entity<Emergency>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(255);
            });


            modelBuilder.Entity<GroupDocument>(entity =>
            {
                entity.ToTable("Group_Documents");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(10);

                entity.Property(e => e.ExpirationYear)
                    .HasColumnType("datetime")
                    .HasColumnName("Expiration_Year");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<NameDocument>(entity =>
            {
                entity.ToTable("Name_Documents");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Sercurity>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Signer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.Signers)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_Signer_Position");
            });

            modelBuilder.Entity<Specialize>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
            });
        }
    }
}
