using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderForUpdatingDTO
    {
        public TableForCreationDTO TableDTO { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }
    }
}
