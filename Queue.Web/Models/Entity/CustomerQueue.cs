namespace Queue.Web.Models.Entity
{
    public partial class CustomerQueue
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string? Name { get; set; }

        public HashSet<Customer>? Customers { get; set; } = null!;

    }
}
