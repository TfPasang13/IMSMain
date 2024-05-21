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
    public class UnitInfoConfiguration : IEntityTypeConfiguration<UnitInfo>
    {
        public void Configure(EntityTypeBuilder<UnitInfo> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.UnitName)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.HasMany(e => e.ProductInfos)
                .WithOne(pt => pt.UnitInfo)
                .HasForeignKey(e => e.UnitInfoId);

            builder.HasMany(e => e.TransactionInfos)
               .WithOne(pt => pt.UnitInfo)
               .HasForeignKey(e => e.UnitInfoId);

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
