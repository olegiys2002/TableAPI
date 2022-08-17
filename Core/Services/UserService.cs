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
using SharedAssembly;
using Shared;
using Microsoft.AspNetCore.Http;

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
            byte[] imageData;
            var image = userForCreationDTO.AvatarFormDTO.Image;
            imageData = Image.ImageInBytes(image);

            user.Avatar = new Avatar()
            {
                Image = imageData
            };

            user.PasswordHash = Hash.HashPassword(userForCreationDTO.Password);
         
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
            var user = await _unitOfWork.UserRepository.GetUser(id);
            if (user == null)
            {
                return null;
            }
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.FindAll(false).ToListAsync();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<AvatarDTO> GetUserAvatar(int id)
        {
           var avatar = await _unitOfWork.UserRepository.GetAvatarAsync(id);
           if (avatar == null)
           {
                return null;
           }
           var avatarDTO = _mapper.Map<AvatarDTO>(avatar);
           return avatarDTO;
        }
        public async Task<bool> UpdateUser(int id,UserFormDTO userForUpdatingDTO)
        {
            User user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return false;
            }

            byte[] imageData;
            var image = userForUpdatingDTO.AvatarFormDTO.Image;
            imageData = Image.ImageInBytes(image);

            user.Avatar = new Avatar()
            {
                Image = imageData
            };

            user.Name = userForUpdatingDTO.Name;
            user.Role = userForUpdatingDTO.Role;
            user.Email = userForUpdatingDTO.Email;
            user.PasswordHash = Hash.HashPassword(userForUpdatingDTO.Password);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
