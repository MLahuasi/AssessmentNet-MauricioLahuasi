using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Entity.DTOs
{
    public class QueueInfo
    {
        public int QueueId { get; set; }
        public string? QueueName { get; set; }

        public int? TotalQueueDuration { get; set; }

        public int? CustomerCount { get; set; }
    }
}
