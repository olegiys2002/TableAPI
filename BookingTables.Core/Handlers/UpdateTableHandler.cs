using AutoMapper;
using Core.Commands;
using Core.DTOs;
using Core.IServices;
using MediatR;

namespace Core.Handlers
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, TableDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateTableHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TableDTO> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await _unitOfWork.TableRepository.GetTableAsync(request.Id);
            

            if (table == null)
            {
                return null;
            }
            var tableForUpdateDTO = request.TableFormDTO;

            table.CountOfSeats = tableForUpdateDTO.CountOfSeats;
            table.Number = tableForUpdateDTO.Number;

            await _unitOfWork.SaveChangesAsync();

            var tableDTO = _mapper.Map<TableDTO>(table);
            return tableDTO;
        }
    }
}
