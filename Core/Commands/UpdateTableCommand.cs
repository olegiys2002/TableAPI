using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public class UpdateTableCommand : IRequest<bool>
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
