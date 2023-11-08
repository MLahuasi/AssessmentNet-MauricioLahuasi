using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Queue.Web.Models.Entity.Config
{
    public class CustomerQueueConfig : IEntityTypeConfiguration<CustomerQueue>
    {
        public void Configure(EntityTypeBuilder<CustomerQueue> builder)
        {
            // CONFIGURAR PK
            builder.HasKey(prop => prop.Id);
        }
    }
}
