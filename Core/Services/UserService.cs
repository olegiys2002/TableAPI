using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using SharedAssembly;
using Shared;
using User = Core.Models.User;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly ICacheService<List<User>> _cacheService;
        private readonly ICacheService<User> _cacheUserService;
        private readonly string _userKeyCaching="userCache";
        public UserService(IUnitOfWork unitOfWork,IMapper mapper,IStorage storage,ICacheService<User> cacheUserService,ICacheService<List<User>> cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
            _cacheUserService = cacheUserService;
            _cacheService = cacheService;
        }
        public async Task<UserDTO> CreateUser(UserFormDTO userForCreationDTO)
        {
            var user = _mapper.Map<User>(userForCreationDTO);
            byte[] imageData;
            if (userForCreationDTO.AvatarFormDTO.Image == null)
            {
                return null;
            }
            var image = userForCreationDTO.AvatarFormDTO.Image;
            imageData = Image.ImageInBytes(image);

            
            user.Avatar = new Avatar()
            {
                Image = imageData
            };

            await _storage.CreateAvatarAsync(userForCreationDTO.AvatarFormDTO.Image);
            
           
            user.PasswordHash = Hash.HashPassword(userForCreationDTO.Password);
         
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheUserService.CacheItems(user.Id.ToString(), user);
            await _cacheService.RemoveCache(_userKeyCaching);
            
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
            
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return false;
            }
            _unitOfWork.UserRepository.Delete(user);
            await _cacheUserService.RemoveCache(id.ToString());
            await _cacheService.RemoveCache(_userKeyCaching);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _cacheUserService.TryGetCache(id.ToString());
            if (user == null)
            {
                user = await _unitOfWork.UserRepository.GetUser(id);
                if (user == null)
                {
                    return null;
                }
                await _cacheUserService.CacheItems(id.ToString(), user);
            }
                var userDTO = _mapper.Map<UserDTO>(user);
           
            return userDTO;
        }

        public async Task<List<UserDTO>> GetUsers()
        {

            var users = await _cacheService.TryGetCache(_userKeyCaching);
            
            if (users == null)
            {
                users = await _unitOfWork.UserRepository.FindAll(false).ToListAsync();

                if (users.Count == 0)
                {
                    return null;
                }

                await _cacheService.CacheItems(_userKeyCaching, users);
            }
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
            var user = await _unitOfWork.UserRepository.GetUser(id);

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
