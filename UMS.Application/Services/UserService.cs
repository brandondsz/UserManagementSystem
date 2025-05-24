using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Application.DTOs;
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

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(UserDto dto)
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
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;

            await _repository.UpdateAsync(user);
        }

        public IList<UserDto> GetAll()
        {
            var userQuery = _repository.GetAllIQueryable();
            return userQuery.Select(u => new UserDto
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Id = u.Id
            }).ToList();
        }
    }
}
