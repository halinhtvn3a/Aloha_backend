﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlohaVietnam.Repositories.Models;

public partial class AlohaVietnamContext : IdentityDbContext<User>
{
    public AlohaVietnamContext(DbContextOptions<AlohaVietnamContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-RGB0PJ4;Database=Alohavietnam;uid=sa;pwd=12345;TrustServerCertificate=True;");
        }
    }
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Clue> Clues { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<SideQuest> SideQuests { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<UserProgress> UserProgresses { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07CCC4AF34");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07CCC4AF34");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Clue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clues__3214EC07493E4063");

            entity.HasIndex(e => e.PackageId, "IX_Clues_PackageId");

            entity.Property(e => e.AnswerCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Question).IsRequired();

            entity.HasOne(d => d.Package).WithMany(p => p.Clues)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clues__PackageId__3C69FB99");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC07D62C2484");

            entity.ToTable("Feedback");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);

            entity.HasOne(d => d.Package).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Packag__6B24EA82");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC071EF357C4");

            entity.HasIndex(e => e.CityId, "IX_Packages_CityId");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.City).WithMany(p => p.Packages)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Packages__CityId__398D8EEE");
        });

        modelBuilder.Entity<SideQuest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SideQues__3214EC078EB65152");

            entity.HasIndex(e => e.PackageId, "IX_SideQuests_PackageId");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Package).WithMany(p => p.SideQuests)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SideQuest__Packa__3F466844");
        });
        

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC075182CE09");

            entity.HasIndex(e => e.CityId, "IX_SubscriptionPlans_CityId");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.City).WithMany(p => p.SubscriptionPlans)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__CityI__4222D4EF");
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProg__3214EC070830F6B1");

            entity.ToTable("UserProgress");

            entity.Property(e => e.CompletionTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);

            entity.HasOne(d => d.Clue).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.ClueId)
                .HasConstraintName("FK__UserProgr__ClueI__656C112C");

            entity.HasOne(d => d.Package).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserProgr__Packa__6477ECF3");

            entity.HasOne(d => d.SideQuest).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.SideQuestId)
                .HasConstraintName("FK__UserProgr__SideQ__66603565");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubs__3214EC072D2252EC");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);

            entity.HasOne(d => d.SubscriptionPlan).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSubsc__Subsc__5EBF139D");
        });
        modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "R001",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "R002",
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "R003",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}