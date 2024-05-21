using IMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace IMS.Infrastructure.EntityConfiguration
{
    
    public class StoreConfiguration : IEntityTypeConfiguration<StoreInfo>
    {
        public void Configure(EntityTypeBuilder<StoreInfo> builder)
        {
           
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Property(e => e.StoreName)
            .HasMaxLength(200)
            .IsUnicode(true);

            builder.Property(e => e.Address)
            .HasMaxLength(200)
            .IsUnicode(true);

            builder.Property(e => e.PhoneNumber)
            .HasMaxLength(30)
            .IsUnicode(true);

            builder.Property(e => e.RegistrationNo)
            .HasMaxLength(30)
            .IsUnicode(true);

            builder.HasMany(e => e.CatagoryInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.CustomerInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.ProductInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.ProductInvoiceInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.ProductRateInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.RackInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.StockInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.SuppliersInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.HasMany(e => e.TransactionInfos)
                .WithOne(pt => pt.StoreInfo)
                .HasForeignKey(e => e.StoreInfoId);

            builder.Property(e => e.IsActive) 
            .IsRequired();


            builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.CreatedBy)
            .IsRequired()
            .IsUnicode (true);

            builder.Property(e => e.ModifiedDate)
            .HasColumnType("datetime");
            


            builder.Property(e => e.ModifiedBy)
            .IsUnicode(true);



        }
    }
}
