using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(false); 
            builder.Property(x => x.Surname).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.MessageDescription).HasMaxLength(255).IsRequired(false);
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.Phone).IsRequired(false).HasMaxLength(20);

        }
    }
}
