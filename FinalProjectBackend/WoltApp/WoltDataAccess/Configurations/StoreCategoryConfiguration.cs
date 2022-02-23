using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class StoreCategoryConfiguration : IEntityTypeConfiguration<StoreCategory>
    {
        public void Configure(EntityTypeBuilder<StoreCategory> builder)
        {
            builder.HasOne(rc => rc.Store)
                   .WithMany(r => r.StoreCategories)
                   .HasForeignKey(r => r.StoreId);
            builder.HasOne(rc => rc.Category)
                   .WithMany(c => c.StoreCategories)
                   .HasForeignKey(c => c.CategoryId);
        }
    }
}
