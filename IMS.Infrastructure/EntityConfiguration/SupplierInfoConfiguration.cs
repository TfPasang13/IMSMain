using IMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.EntityConfiguration
{
    public class SupplierInfoConfiguration : IEntityTypeConfiguration<SupplierInfo>
    {
        public void Configure(EntityTypeBuilder<SupplierInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.SupplierName)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.ContactPerson)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.PhoneNumber)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.Address)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.Property(e => e.Email)
          .HasMaxLength(200)
          .IsUnicode(true);

            builder.HasOne(e => e.StoreInfo)
                .WithMany(pt => pt.SuppliersInfos)
                .HasForeignKey(e => e.StoreInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ProductRateInfos)
                .WithOne(pt => pt.SupplierInfo)
                .HasForeignKey(e =>e.SupplierInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.IsActive) 
          .IsRequired();


            builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.CreatedBy)
            .IsRequired()
            .IsUnicode(true);

            builder.Property(e => e.ModifiedDate)
            .HasColumnType("datetime");



            builder.Property(e => e.ModifiedBy)
            .IsUnicode(true);
        }
    }
}
