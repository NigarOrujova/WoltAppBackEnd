using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class RestaurantCategoryConfiguration : IEntityTypeConfiguration<RestaurantCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            builder.HasOne(rc=>rc.Restaurant)
                   .WithMany(r => r.RestaurantCategories)
                   .HasForeignKey(r=>r.RestaurantId);
            builder.HasOne(rc=>rc.Category)
                   .WithMany(c => c.RestaurantCategories)
                   .HasForeignKey(c=>c.CategoryId);
        }
    }
}
