using Core.DTOs;
using MediatR;
using BookingTables.Shared.RequestModels;

namespace Core.Queries
{
    public class GetTablesQuery : IRequest<IEnumerable<TableDTO>>
    {
        public TableRequest TableRequest { get; set; }
        public GetTablesQuery(TableRequest tableRequest)
        {
            TableRequest = tableRequest;
        }
    }
}
