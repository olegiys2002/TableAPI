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
        [Required(ErrorMessage = "Table id is a required field")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "CountOfPeople is a required field")]
        public int CountOfPeople { get; set; }

        [Required(ErrorMessage = "Data of reservation is a required field")]
        public DateTime DateOfReservation { get; set; }
    }
}
