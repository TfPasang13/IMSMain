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
    public class TransactionInfoConfiguration : IEntityTypeConfiguration<TransactionInfo>
    {
        public void Configure(EntityTypeBuilder<TransactionInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.TransactionType)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Quantity)
                .HasColumnType("float");

            builder.Property(e => e.Rate)
                .HasColumnType("float");

            builder.Property(e => e.TotalAmount)
                .HasColumnType("float");

            builder.HasOne(e => e.CatagoryInfo)
                .WithMany(pt => pt.TransactionInfos)
                .HasForeignKey(e => e.CatagoryInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductInfo)
                .WithMany(pt => pt.TransactionInfos)
                .HasForeignKey(e => e.ProductInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductRateInfo)
                .WithMany(pt => pt.TransactionInfos)
                .HasForeignKey(e => e.ProductRateInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.StoreInfo)
                .WithMany(pt => pt.TransactionInfos)
                .HasForeignKey(e => e.StoreInfoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UnitInfo)
                .WithMany(pt => pt.TransactionInfos)
                .HasForeignKey(e => e.UnitInfoId)
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
