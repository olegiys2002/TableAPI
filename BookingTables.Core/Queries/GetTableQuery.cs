using Core.DTOs;
using MediatR;

namespace Core.Queries
{
    public class GetTableQuery : IRequest<TableDTO>
    {
        public int Id { get; set; }
        public GetTableQuery(int id)
        {
            Id = id;
        }
    }
}
