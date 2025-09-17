using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var userResult = await _userManager.CreateAsync(user, dto.Password);

            if (userResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, SystemRoles.User);
                if (!roleResult.Succeeded)
                    throw new UserNotCreatedException();
            }
            else
                throw new UserNotCreatedException();
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task EditUserAsync(EditUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
