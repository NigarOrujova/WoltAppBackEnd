using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
    {
        public void Configure(EntityTypeBuilder<StoreProduct> builder)
        {
            builder.HasOne(rp => rp.Store)
                   .WithMany(r => r.StoreProducts)
                   .HasForeignKey(r => r.StoreId);
            builder.HasOne(rp => rp.Product)
                   .WithMany(p => p.StoreProducts)
                   .HasForeignKey(p => p.ProductId);
        }
    }
}
