using BookingTables.Shared.SortModels;

namespace BookingTables.Shared.RequestModels
{
    public abstract class RequestFeatures
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public SortModel []? SortModel { get; set; }
    }
}
