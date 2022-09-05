using AutoMapper;
using Core.Commands;
using Core.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handlers
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTableHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await _unitOfWork.TableRepository.GetTable(request.Id);
            var tableForUpdateDTO = request.TableFormDTO;
            if (table == null)
            {
                return false;
            }

            table.CountOfSeats = tableForUpdateDTO.CountOfSeats;
            table.Number = tableForUpdateDTO.Number;


            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
