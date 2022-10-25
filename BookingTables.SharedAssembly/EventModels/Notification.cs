namespace BookingTables.Shared.EventModels
{
    public class Notification
    {
        public string Email { get; set; }
        public List<int> Tables { get; set; }
    }
}
