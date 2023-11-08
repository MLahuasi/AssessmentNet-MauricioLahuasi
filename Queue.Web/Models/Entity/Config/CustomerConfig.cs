using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Queue.Web.Models.Entity.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // CONFIGURAR PK
            builder
                .HasKey(prop => prop.Id);
        }
    }
}
