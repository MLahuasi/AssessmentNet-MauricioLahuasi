using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.EF.EntityConfig
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
