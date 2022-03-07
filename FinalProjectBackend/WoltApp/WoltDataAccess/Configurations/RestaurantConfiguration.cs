using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ControllerName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255).IsRequired(false);
            builder.Property(p => p.Address).HasMaxLength(255).IsRequired(false);
            builder.Property(p => p.ContactNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.ImageURL).IsRequired();
            builder.Ignore(x => x.HeroPhoto);
            builder.Property(x => x.HeroImageURL).IsRequired();
            builder.Ignore(x => x.Photo);
            builder.Ignore(x => x.CategoryIds);
            builder.Ignore(x => x.ProductIds);
            builder.Property(x => x.IsActive).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
