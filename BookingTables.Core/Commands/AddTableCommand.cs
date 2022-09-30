using Core.DTOs;
using MediatR;

namespace Core.Commands
{
    public class AddTableCommand : IRequest<TableDTO>
    {
        public TableFormDTO TableForm { get; set; }
        public AddTableCommand(TableFormDTO tableFormDTO)
        {
            TableForm = tableFormDTO;
        }
    }
}
