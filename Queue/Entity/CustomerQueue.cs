using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Entity.DTOs;

namespace Queue.Entity
{
    public partial class CustomerQueue
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string? Name { get; set; }

        public HashSet<Customer>? Customers { get; set; } = null!;

    }
}
