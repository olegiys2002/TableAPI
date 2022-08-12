using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderForUpdatingDTO
    {
        [Required(ErrorMessage = "Table id is a required field")]
        public int TableId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }
    }
}
