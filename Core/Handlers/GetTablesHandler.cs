using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handlers
{
    public class GetTablesHandler : IRequestHandler<GetTablesQuery, IEnumerable<TableDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTablesHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TableDTO>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await _unitOfWork.TableRepository.FindAll(false).ToListAsync();
            var tablesDTO = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTO;

        }
    }
}
