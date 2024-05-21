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
    public class ProductInfoConfiguration : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
           .HasMaxLength(200)
           .IsUnicode(true);

            builder.Property(e => e.ProductDescription)
           .HasMaxLength(200)
           .IsUnicode(true);

            builder.Property(e => e.ImgageUrl)
           .HasMaxLength(200)
           .IsUnicode(true);

            builder.HasOne(e => e.CatagoryInfo)
                .WithMany(pt => pt.ProductInfos)
                .HasForeignKey(e => e.CatagoryInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UnitInfo)
                .WithMany(pt => pt.ProductInfos)
                .HasForeignKey(e => e.UnitInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StoreInfo)
                .WithMany(pt => pt.ProductInfos)
                .HasForeignKey(e => e.StoreInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ProductRateInfos)
               .WithOne(pt => pt.ProductInfo)
               .HasForeignKey(e => e.ProductInfoId);

            builder.HasMany(e => e.StockInfos)
               .WithOne(pt => pt.ProductInfo)
               .HasForeignKey(e => e.ProductInfoId);

            builder.HasMany(e => e.TransactionInfos)
               .WithOne(pt => pt.ProductInfo)
               .HasForeignKey(e => e.ProductInfoId);

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
