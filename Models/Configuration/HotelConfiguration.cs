using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Models.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotels>
    {
        public void Configure(EntityTypeBuilder<Hotels> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
        }
    }
}
