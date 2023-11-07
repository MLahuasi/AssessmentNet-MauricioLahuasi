using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Entity
{
    public class Customer
    {
        public int Id { get; set; }

        public string Ci { get; set; }
        public string Name { get; set; }

        public string? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public Customer(string ci, string name)
        {
            Ci = ci;
            Name = name;
        }

        public override string ToString()
        {
            return $" Id: {Id}, Name: {Name}, Ci: {Ci}, Status: {Status}, StartDate: {StartDate}, FinishDate: {FinishDate}, Queue.Id: {Queue.Id}, Queue.Name: {Queue.Name}, Queue.Duration: {Queue.Duration}";
        }

        public CustomerQueue Queue { get; set; } = null!;
    }
}
