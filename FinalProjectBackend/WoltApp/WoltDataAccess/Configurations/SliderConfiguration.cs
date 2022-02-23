using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255).IsRequired(false);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.ImageURL).IsRequired();
            builder.Ignore(p => p.Photo);
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
