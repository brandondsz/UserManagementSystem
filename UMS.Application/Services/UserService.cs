using UMS.Application.DTOs;
using UMS.Application.Mappers;
using UMS.Core.Entities;
using UMS.Core.Interfaces;

namespace UMS.Application.Services
{

    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var user =  await _repository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User {id} not found.");
            }
            return UserMapper.ToDto(user);
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            await _repository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(Guid id, UserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User {id} not found.");
            }
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;

            await _repository.UpdateAsync(user);
        }

        public IList<UserDto> GetAll()
        {
            var userQuery = _repository.GetAllIQueryable();
            return userQuery.Select(u => UserMapper.ToDto(u)).ToList();
        }
    }
}
