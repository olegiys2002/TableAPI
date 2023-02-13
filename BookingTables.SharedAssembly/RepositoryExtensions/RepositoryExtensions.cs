using BookingTables.Shared.SortModels;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BookingTables.Shared.RepositoriesExtensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            return queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> expression)
        {
            return queryable.Where(expression);
        }
        public static IQueryable<T> SortItems<T>(this IQueryable<T> queryable, SortModel []sortModel)
        {
            if (sortModel == null)
            {
                return queryable;
            }
            if (sortModel.Count()==0)
            {
                return queryable;
            }
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQuaryBuilder = new StringBuilder();

            foreach (var model in sortModel)
            {
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(model.Field, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }
                var direction = model.OrderSort.Equals("desc") ? "descending" : "ascending";
                orderQuaryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }
            var orderQuery = orderQuaryBuilder.ToString().TrimEnd(',',' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return queryable;
            }

            return queryable.OrderBy(orderQuery);

        }

    }
}
