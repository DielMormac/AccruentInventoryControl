using AccruentInventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccruentInventoryControl.Infrastructure.Configurations
{
    public static class WarehouseTransactionConfiguration
    {
        public static void Configure(EntityTypeBuilder<WarehouseTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.PreviousQuantity)
                .IsRequired();

            builder.Property(x => x.TotalQuantity)
                .IsRequired();
        }
    }
}
