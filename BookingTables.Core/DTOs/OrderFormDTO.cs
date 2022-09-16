using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderFormDTO
    {
        public List<int> TablesId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }
    }
}
