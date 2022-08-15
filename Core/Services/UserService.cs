using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<UserDTO> CreateUser(UserFormDTO userForCreationDTO)
        {
            User user = _mapper.Map<User>(userForCreationDTO);
            user.PasswordHash = HashPassword(userForCreationDTO.Password);
         
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

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
            if (user == null)
            {
                return null;
            }
            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            List<User> users = await _unitOfWork.UserRepository.FindAll(false).ToListAsync();
            List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<bool> UpdateUser(int id,UserFormDTO userForUpdatingDTO)
        {
            User user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return false;
            }
            
            user.Name = userForUpdatingDTO.Name;
            user.Role = userForUpdatingDTO.Role;
            user.Email = userForUpdatingDTO.Email;
            user.PasswordHash = HashPassword(userForUpdatingDTO.Password);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = SHA256.HashData(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

    }
}
