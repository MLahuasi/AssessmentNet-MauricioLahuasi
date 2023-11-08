namespace Queue.Web.Models.Entity.DTOs
{
    public class QueueInfo
    {
        public int QueueId { get; set; }
        public string? QueueName { get; set; }

        public int? TotalQueueDuration { get; set; }

        public int? CustomerCount { get; set; }
    }
}
