
namespace Models.Models
{
    public class Order : Entity
    {
        public List<Table> Table { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }

    }
}
    