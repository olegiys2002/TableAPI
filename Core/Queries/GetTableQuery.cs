using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
