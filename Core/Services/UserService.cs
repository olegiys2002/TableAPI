using Core.DTOs;
using Core.IServices;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDTO> CreateUser(UserForCreationDTO userForCreationDTO)
        {
            User user = _mapper.ToDomainModel(userForCreationDTO);
            user.CreatedAt = DateTime.Now;
          
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();

            UserDTO userDTO = _mapper.ToDTO(user);

            return userDTO;
            
        }

        public async Task<bool> DeleteUser(int id)
        {
            User user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return false;
            }
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            User user = await _unitOfWork.UserRepository.GetUser(id);
            UserDTO userDTO = _mapper.ToDTO(user);

            return userDTO;
        }

        public List<UserDTO> GetUsers()
        {
            List<User> users = _unitOfWork.UserRepository.FindAll(false).ToList();
            List<UserDTO> userDTOs = _mapper.ToListDTO(users);

            return userDTOs;
        }

        public async Task<bool> UpdateUser(int id,UserForUpdatingDTO userForUpdatingDTO)
        {
            User user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return false;
            }

            user.Name = userForUpdatingDTO.Name;
            user.Role = userForUpdatingDTO.Role;
            user.UpdatedAt = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
