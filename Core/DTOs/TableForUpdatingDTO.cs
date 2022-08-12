using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TableForUpdatingDTO
    {
        [Required(ErrorMessage = "Number is a required field")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required(ErrorMessage = "Count of Seats is a required field")]
        [Range(1,100)]
        public int CountOfSeats { get; set; }
    }
}
