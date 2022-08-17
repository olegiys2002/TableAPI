using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order : Entity
    {
        public IList<Table> Table { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }

    }
}
    