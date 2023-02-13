

namespace Models.Models
{
    public class Table : Entity
    {
        public int Number { get; set; }
        public int CountOfSeats { get; set; }
        public List<Order> Orders { get; set; }
        public decimal Cost { get; set; }

    }
}
