using Core.DTOs;
using MediatR;

namespace Core.Commands
{
    public class UpdateTableCommand : IRequest<TableDTO>
    {
        public TableFormDTO TableFormDTO { get; set; }
        public int Id { get; set; }
        public UpdateTableCommand(TableFormDTO tableFormDTO,int id)
        {
            TableFormDTO = tableFormDTO;
            Id = id;

        }
    }
}
