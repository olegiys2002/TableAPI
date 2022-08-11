using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TableDTO TableDTO { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }

    }
}
