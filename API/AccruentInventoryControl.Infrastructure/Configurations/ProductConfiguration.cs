using AccruentInventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccruentInventoryControl.Infrastructure.Configurations
{
    public static class ProductConfiguration
    {
        public static void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.Code, "UQ_Product_Code")
                .IsUnique();
        }
    }
}
