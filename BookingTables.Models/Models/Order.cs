
namespace Models.Models
{
    public class Order : Entity
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<Table> Table { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DateOfReservation { get; set; }

    }
}
    