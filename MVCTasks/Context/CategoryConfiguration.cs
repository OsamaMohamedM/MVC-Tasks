using Microsoft.EntityFrameworkCore;

namespace MVCTasks.Context
{
    public class CategoryConfiguration : IEntityTypeConfiguration<MVCTasks.Models.Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MVCTasks.Models.Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
        }
    }
}