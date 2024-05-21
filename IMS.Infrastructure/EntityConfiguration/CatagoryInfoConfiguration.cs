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
    public class CatagoryInfoConfiguration : IEntityTypeConfiguration<CatagoryInfo>
    {

        public void Configure(EntityTypeBuilder<CatagoryInfo> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.CatagoryName)
           .HasMaxLength(200)
           .IsUnicode(true);

            builder.Property(e => e.CatagoryDescription)
           .HasMaxLength(200)
           .IsUnicode(true);

            builder.HasOne(e => e.StoreInfo)
                .WithMany(pt => pt.CatagoryInfos)
                .HasForeignKey(e => e.StoreInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ProductInfos)
               .WithOne(pt => pt.CatagoryInfo)
               .HasForeignKey(e => e.CatagoryInfoId);

            builder.HasMany(e => e.ProductRateInfos)
              .WithOne(pt => pt.CatagoryInfo)
              .HasForeignKey(e => e.CatagoryInfoId);

            builder.HasMany(e => e.StockInfos)
              .WithOne(pt => pt.CatagoryInfo)
              .HasForeignKey(e => e.CatagoryInfoId);

            builder.HasMany(e => e.TransactionInfos)
              .WithOne(pt => pt.CatagoryInfo)
              .HasForeignKey(e => e.CatagoryInfoId);

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
