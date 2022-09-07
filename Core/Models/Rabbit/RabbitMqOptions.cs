using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Rabbit
{
    public class RabbitMqOptions
    {
        public const string RabbitMq = "RabbitMq";
        public string HostName { get; set; }
        public string Queue { get; set; }
    }
}
