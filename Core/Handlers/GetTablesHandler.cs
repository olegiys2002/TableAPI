using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
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
        private readonly ICacheService<List<Table>> _cacheService;
        public GetTablesHandler(IUnitOfWork unitOfWork,IMapper mapper, ICacheService<List<Table>> cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<IEnumerable<TableDTO>> Handle(GetTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await _unitOfWork.TableRepository.FindAll(false).ToListAsync();
            var tablesDTO = _mapper.Map<List<TableDTO>>(tables);
            return tablesDTO;

        }
    }
}
