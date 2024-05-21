using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMS.web.Data
{
    public class AppDbContext:IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbcontext) 
            : base(dbcontext) 
        { 
        }

        public DbSet<AppUser>AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<AppUser>()
                .Property(e => e.MiddleName)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.LastName)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.StoreId)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.Address)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.UserRoleId)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.ProfilePicture)
                .HasMaxLength(255);

            builder.Entity<AppUser>()
                .Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .HasMaxLength(255);

            builder.Entity<IdentityRole>()
                .ToTable("Roles")
                .Property(p => p.Id)
                .HasColumnName("RoleId");

        }

    }
}
