using AutoMapper;
using Core.Commands;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handlers
{
    public class AddTableHandler : IRequestHandler<AddTableCommand, TableDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTableHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TableDTO> Handle(AddTableCommand request, CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Table>(request.TableForm);
            _unitOfWork.TableRepository.Create(table);
            await _unitOfWork.SaveChangesAsync();
            var tableDTO = _mapper.Map<TableDTO>(table);
            return tableDTO;
        }
    }
}
