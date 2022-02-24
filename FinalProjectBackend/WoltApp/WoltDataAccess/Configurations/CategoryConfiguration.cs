using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WoltEntity.Entities;

namespace WoltDataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ControllerName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.ImageURL).IsRequired(false);
            builder.Ignore(p => p.Photo);
        }
    }
}
