    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Table : Entity
    {
        public int Number { get; set; }
        public int CountOfSeats { get; set; }
        public List<Order> Orders { get; set; }

    }
}
