using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class RestaurantProductConfiguration : IEntityTypeConfiguration<RestaurantProduct>
    {
        public void Configure(EntityTypeBuilder<RestaurantProduct> builder)
        {
            builder.HasOne(rp => rp.Restaurant)
                   .WithMany(r => r.RestaurantProducts)
                   .HasForeignKey(r => r.RestaurantId);
            builder.HasOne(rp => rp.Product)
                   .WithMany(p => p.RestaurantProducts)
                   .HasForeignKey(p => p.ProductId);
        }
    }
}
