using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentManager.Models
{
    public partial class ProjectPrn221Context : DbContext
    {
        public ProjectPrn221Context()
        {
        }

        public ProjectPrn221Context(DbContextOptions<ProjectPrn221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AgencyIssued> AgencyIssueds { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Emergency> Emergencies { get; set; } = null!;
        public virtual DbSet<Feture> Fetures { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupDocument> GroupDocuments { get; set; } = null!;
        public virtual DbSet<NameDocument> NameDocuments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Sercurity> Sercurities { get; set; } = null!;
        public virtual DbSet<Signer> Signers { get; set; } = null!;
        public virtual DbSet<Specialize> Specializes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
			{
				var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
				optionsBuilder.UseSqlServer(ConnectionString);
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<AgencyIssued>(entity =>
            {
                entity.ToTable("Agency_Issued");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AgencyIssuedId).HasColumnName("Agency_issued_id");

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

                entity.Property(e => e.EmergencyId).HasColumnName("Emergency_id");

                entity.Property(e => e.GroupDocumentId).HasColumnName("Group_DocumentId");

                entity.Property(e => e.NameDocumentId).HasColumnName("Name_DocumentId");

                entity.Property(e => e.Receiver).HasMaxLength(255);

                entity.Property(e => e.SecurityId).HasColumnName("Security_id");

                entity.Property(e => e.SignerId).HasColumnName("Signer_id");

                entity.Property(e => e.SpecializedId).HasColumnName("Specialized_id");

                entity.Property(e => e.Symbol).HasMaxLength(255);

                entity.HasOne(d => d.AgencyIssued)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.AgencyIssuedId)
                    .HasConstraintName("FK_Documents_Agency_Issued");

                entity.HasOne(d => d.AidNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.Aid)
                    .HasConstraintName("FK_Documents_Account");

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
            });

            modelBuilder.Entity<Emergency>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Feture>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(d => d.Aids)
                    .WithMany(p => p.Gids)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupAccount",
                        l => l.HasOne<Account>().WithMany().HasForeignKey("Aid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupAccounts_Account"),
                        r => r.HasOne<Group>().WithMany().HasForeignKey("Gid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupAccounts_Groups"),
                        j =>
                        {
                            j.HasKey("Gid", "Aid");

                            j.ToTable("GroupAccounts");
                        });

                entity.HasMany(d => d.Fids)
                    .WithMany(p => p.Gids)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupFeture",
                        l => l.HasOne<Feture>().WithMany().HasForeignKey("Fid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupFetures_Fetures"),
                        r => r.HasOne<Group>().WithMany().HasForeignKey("Gid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupFetures_Groups"),
                        j =>
                        {
                            j.HasKey("Gid", "Fid");

                            j.ToTable("GroupFetures");
                        });
            });

            modelBuilder.Entity<GroupDocument>(entity =>
            {
                entity.ToTable("Group_Document");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(10);

                entity.Property(e => e.ExpirationYear)
                    .HasColumnType("datetime")
                    .HasColumnName("Expiration_Year");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<NameDocument>(entity =>
            {
                entity.ToTable("Name_Document");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Sercurity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Signer>(entity =>
            {
                entity.ToTable("Signer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.Signers)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_Signer_Position");
            });

            modelBuilder.Entity<Specialize>(entity =>
            {
                entity.ToTable("Specialize");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
