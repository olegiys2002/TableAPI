using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Queries;
using MediatR;

namespace Core.Handlers
{
    public class GetTableHandler : IRequestHandler<GetTableQuery, TableDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTableHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TableDTO> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
          var table = await _unitOfWork.TableRepository.GetTableAsync(request.Id);
          var tableDTO =  _mapper.Map<TableDTO>(table);
          return tableDTO;
        }
    }
}
