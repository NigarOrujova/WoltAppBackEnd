using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255).IsRequired(false);
            builder.Property(p => p.Price).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Count).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DiscountPercent).IsRequired().HasDefaultValue(0).HasColumnType("decimal(18,2)");
            builder.Property(p =>p.ImageURL).IsRequired();
            builder.Ignore(p =>p.Photo);
            builder.Ignore(p =>p.BasketCount);
            builder.Ignore(x => x.RestaurantIds);
            builder.Ignore(x => x.StoreIds);
            builder.Property(p => p.IsActive).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(p => p.Category).WithMany(x => x.Products);
        }
    }
}
