namespace Core.DTOs
{
    public class OrderDTO : DTO
    {
        public List<TableDTO> Tables { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime StartOfReservation { get; set; }
        public DateTime EndOfReservation { get; set; }
        public decimal CostOfOrder { get; set; }
    }
}
