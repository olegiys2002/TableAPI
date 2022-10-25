namespace BookingTables.Shared.RequestModels
{
    public class TableRequest : RequestFeatures
    {
        public int MinCountOfSeats { get; set; } = 0;
        public int MaxCountOfSeats { get; set; } = 100;
        
    }
}
