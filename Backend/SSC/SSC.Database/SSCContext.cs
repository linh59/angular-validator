using Microsoft.EntityFrameworkCore;
using SSC.Core.Base.Infrastructure;
using SSC.Database.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace SSC.Database
{
    public class SSCContext : BaseDbContext
    {
        public SSCContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        #region Database Tables Collections
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<CreatedControl> CreatedControls { get; set; }
        public DbSet<CreatedForm> CreatedForms { get; set; }
        public DbSet<CreatedTableControl> CreatedTableControls { get; set; }
        public DbSet<FormAttribute> FormAttributes { get; set; }
        public DbSet<FormControl> FormControls { get; set; }
        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormTemplateAttribute> FormTemplateAttributes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<TableValue> TableValues { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ValueFilled> ValueFilleds { get; set; }
        #endregion

        #region Overriding Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CreatedForm>().HasMany(x => x.CreatedControls).WithOne(x => x.BelongTo);
            modelBuilder.Entity<CreatedForm>().HasMany(x => x.CreatedTableControls).WithOne(x => x.BelongToTable);

            modelBuilder.Entity<FormTemplate>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<FormTemplate>().HasMany(x => x.Controls).WithOne(x => x.BelongTo);
            modelBuilder.Entity<FormTemplate>().HasMany(x => x.TableControls).WithOne(x => x.BelongToTable);
            
            modelBuilder.Entity<User>().HasIndex(x => x.UserCode).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();

            modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.Roles).HasForeignKey(x => x.UserId);


            #region Seeding
            //var category = new Category()
            //{
            //    CreatedAt = DateTime.Now,
            //    Description = "Bao gồm các đơn cho phép đặt từ xa",
            //    Id = 1,
            //    Name = "Đặt dịch vụ",
            //    LastModifiedAt = DateTime.Now
            //};
            //modelBuilder.Entity<Category>().HasData(category);
            //modelBuilder.Entity<FormTemplate>().HasData(new FormTemplate()
            //{
            //    Category = category,
            //    Controls = new List<Control>()
            //    {
            //        new Control()
            //        {
            //            CreatedAt = DateTime.Now,
            //            Hint = "",
            //            Id = Guid.Parse("7cc755db-b7d6-4fd7-91f8-6b3c3cd09cd3"),
            //            LastModifiedAt = DateTime.Now,
            //            Placeholder = "Vui lòng nhập số lượng",
            //            Style = "material",
            //            Title = "Số lượng"
            //        }
            //    },
            //    CreatedAt = DateTime.Now,
            //    Creator = "ThongHM",
            //    Description = "Đơn đặt nước",
            //    Id = 1,
            //    LastModifiedAt = DateTime.Now,
            //    Name = "Đơn đặt nước",
            //});
            #endregion
        }
        #endregion

    }
}
