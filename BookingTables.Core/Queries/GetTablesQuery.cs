using Core.DTOs;
using MediatR;

namespace Core.Queries
{
    public class GetTablesQuery : IRequest<IEnumerable<TableDTO>>
    {

    }
}
