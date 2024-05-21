using IMS.Infrastructure.EntityConfiguration;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure
{
    public class IMSDbContext : DbContext
    {
        public IMSDbContext(DbContextOptions<IMSDbContext> Options)
            : base(Options)
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StoreConfiguration());
            builder.ApplyConfiguration(new CatagoryInfoConfiguration());
            builder.ApplyConfiguration(new CustomerInfoConfiguration());
            builder.ApplyConfiguration(new UnitInfoConfiguration());
            builder.ApplyConfiguration(new ProductInfoConfiguration());
            builder.ApplyConfiguration(new RackInfoConfiguration());
            builder.ApplyConfiguration(new TransactionInfoConfiguration());
            builder.ApplyConfiguration(new SupplierInfoConfiguration());
            builder.ApplyConfiguration(new ProductRateInfoConfiguration());
            builder.ApplyConfiguration(new StockInfoConfiguration());
            builder.ApplyConfiguration(new ProductInvoiceInfoConfiguration());
            builder.ApplyConfiguration(new ProductInvoiceDetailInfoConfiguration());

        }

    }
                               
}
