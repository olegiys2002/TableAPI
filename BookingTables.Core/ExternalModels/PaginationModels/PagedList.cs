using Microsoft.EntityFrameworkCore;
namespace Core.Models.PaginationModels
{
    public class PagedList<T> : List<T>
    {
        public Metadata Metadata { get; set; }
        public PagedList(List<T> items,int pageNumber,int pageSize,int count)
        {
            Metadata = new Metadata
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source,int pageNumber,int pageSize)
        {
            var list = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            var count = await source.CountAsync();
            return new PagedList<T>(list, pageNumber, pageSize,count);
        }
    }
}
