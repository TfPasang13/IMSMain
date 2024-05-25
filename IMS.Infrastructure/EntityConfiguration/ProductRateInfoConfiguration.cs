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
    public class ProductRateInfoConfiguration : IEntityTypeConfiguration<ProductRateInfo>
    {
        public void Configure(EntityTypeBuilder<ProductRateInfo> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.CostPrice)
          .HasColumnType("float");

            builder.Property(e => e.SellingPrice)
          .HasColumnType("float");

            builder.Property(e => e.Quantity)
          .HasColumnType("float");

            builder.Property(e => e.SoldQuantity)
          .HasColumnType("float");

            builder.Property(e => e.RemainingQuantity)
          .HasColumnType("float");

            builder.Property(e => e.BatchNo)
          .HasMaxLength(200)
           .IsUnicode(true);

            builder.Property(e => e.PurchasedDate)
          .HasColumnType("datetime");

            builder.Property(e => e.ExpiryDate)
          .HasColumnType("datetime");

            builder.HasOne(e => e.CatagoryInfo)
                .WithMany(pt => pt.ProductRateInfos)
                .HasForeignKey(e => e.CatagoryInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductInfo)
                .WithMany(pt => pt.ProductRateInfos)
                .HasForeignKey(e => e.ProductInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StoreInfo)
                .WithMany(pt => pt.ProductRateInfos)
                .HasForeignKey(e => e.StoreInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SupplierInfo)
                .WithMany(pt => pt.ProductRateInfos)
                .HasForeignKey(e => e.SupplierInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.RackInfo)
                .WithMany(pt => pt.ProductRateInfos)
                .HasForeignKey(e => e.RackInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ProductInvoiceDetailInfos)
                .WithOne(pt => pt.ProductRateInfo)
                .HasForeignKey(e => e.ProductRateInfoId);

            builder.HasMany(e => e.StockInfos)
              .WithOne(pt => pt.ProductRateInfo)
              .HasForeignKey(e => e.ProductRateInfoId);

            builder.HasMany(e => e.TransactionInfos)
              .WithOne(pt => pt.ProductRateInfo)
              .HasForeignKey(e => e.ProductRateInfoId);

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
