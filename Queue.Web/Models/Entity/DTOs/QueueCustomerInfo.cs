﻿namespace Queue.Web.Models.Entity.DTOs
{
    public class QueueCustomerInfo
    {
        public int QueueId { get; set; }
        public string? QueueName { get; set; }
        public int QueueDuration { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerStatus { get; set; }
        public DateTime CustomerStartDate { get; set; }
        public DateTime CustomerFinishDate { get; set; }
    }
}
