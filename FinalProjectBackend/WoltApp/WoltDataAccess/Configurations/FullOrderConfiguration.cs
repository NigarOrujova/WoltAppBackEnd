using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    class FullOrderConfiguration : IEntityTypeConfiguration<FullOrder>
    {
        public void Configure(EntityTypeBuilder<FullOrder> builder)
        {
            builder.Property(x => x.TotalCount).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
