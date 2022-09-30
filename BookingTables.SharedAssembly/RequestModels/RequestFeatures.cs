using BookingTables.Shared.SortModels;

namespace Shared.RequestModels
{
    public abstract class RequestFeatures
    {
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        const int maxPageSize = 50;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public SortModel [] SortModel { get; set; }
    }
}
