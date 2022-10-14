using AutoMapper;
using Core.DTOs;
using Core.IServices;
using SharedAssembly;
using Shared;
using User = Models.Models.User;
using Models.Models;
using Shared.RequestModels;
using BookingTables.Infrastructure.Views;
using Nest;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStorage _storage;
        private readonly ICacheService<List<User>> _cacheService;
        private readonly ICacheService<User> _cacheUserService;
        private readonly IElasticClient _elasticClient;
        private readonly string _userKeyCaching="userCache";
        public UserService(IUnitOfWork unitOfWork,IMapper mapper,IStorage storage,ICacheService<User> cacheUserService,ICacheService<List<User>> cacheService,IElasticClient elasticClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
            _cacheUserService = cacheUserService;
            _cacheService = cacheService;
            _elasticClient = elasticClient;
        }
        public async Task<UserDTO> CreateUserAsync(UserFormDTO userForCreationDTO)
        {
            var user = _mapper.Map<User>(userForCreationDTO);
           
            if (userForCreationDTO.AvatarFormDTO.Image == null)
            {
                return null;
            }

            var image = userForCreationDTO.AvatarFormDTO.Image;
            byte[] imageData;
            imageData = Image.ImageInBytes(image);

            
            user.Avatar = new Avatar()
            {
                Image = imageData
            };

            await _storage.CreateAvatarAsync(userForCreationDTO.AvatarFormDTO.Image);
            
           
            user.PasswordHash = Hash.HashPassword(userForCreationDTO.Password);
         
            _unitOfWork.UserRepository.Create(user);
            var response = await _elasticClient.IndexDocumentAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheUserService.CacheItems(user.Id.ToString(), user);
            await _cacheService.RemoveCache(_userKeyCaching);
            
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
            
        }
        public async Task<int?> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserAsync(id);

            if (user == null)
            {
                return null;
            }
            _unitOfWork.UserRepository.Delete(user);

            await _cacheUserService.RemoveCache(id.ToString());
            await _cacheService.RemoveCache(_userKeyCaching);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _cacheUserService.TryGetCache(id.ToString());
            if (user == null)
            {
                user = await _unitOfWork.UserRepository.GetUserAsync(id);

                if (user == null)
                {
                    return null;
                }

                await _cacheUserService.CacheItems(id.ToString(), user);
            }
                var userDTO = _mapper.Map<UserDTO>(user);
           
            return userDTO;
        }

        public async Task<List<UserDTO>> GetUsersAsync(UserRequest userRequest)
        {

            var users = await _cacheService.TryGetCache(_userKeyCaching);
            
            if (users == null)
            {
                users = await _unitOfWork.UserRepository.FindAllAsync(false,userRequest);
                var result = await _elasticClient.SearchAsync<User>(s => s.Query(
                     q => q.QueryString(
                     d => d.Query('*' + userRequest.SearchWord + '*')
                )));
                if (users.Count == 0)
                {
                    return null;
                }

                await _cacheService.CacheItems(_userKeyCaching, result.Documents.ToList());
            }
            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<AvatarDTO> GetUserAvatarAsync(int id)
        {
           var avatar = await _unitOfWork.UserRepository.GetUserAvatarAsync(id);

           if (avatar == null)
           {
                return null;
           }

           var avatarDTO = _mapper.Map<AvatarDTO>(avatar);
           return avatarDTO;
        }
        public Task<List<UserAvatarsDTO>> GetUserIdWithAvatar()
        {
           return _unitOfWork.UserRepository.GetAvatarsWihtUserId();
        }
        public async Task<UserDTO> UpdateUserAsync(int id,UserFormDTO userForUpdatingDTO)
        {
            var user = await _unitOfWork.UserRepository.GetUserAsync(id);

            if (user == null)
            {
                return null;
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

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }



    }
}
