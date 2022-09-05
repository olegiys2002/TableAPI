using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
