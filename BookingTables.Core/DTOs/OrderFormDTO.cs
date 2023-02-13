namespace Core.DTOs
{
    public class OrderFormDTO
    {
        public List<int> TablesId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime StartOfReservation { get; set; }
        public DateTime EndOfReservation { get; set; }
    }
}
