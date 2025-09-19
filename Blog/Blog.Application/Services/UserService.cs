using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!isRoleExist)
            {
                var result = await _roleManager.CreateAsync(new Role(roleName));
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateUserAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var userResult = await _userManager.CreateAsync(user, dto.Password);

            if (userResult.Succeeded)
            {
                dto.Role ??= SystemRoles.User;
                var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

                return roleResult.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ??
                throw new UserProblemException($"Не найден пользователь с id {id}");
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<UserDTO> EditUserAsync(EditUserDTO dto, string id)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new UserProblemException($"Не найден пользователь с id {id}");

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? roleName = null)
        {
            List<User>? users;
            if (roleName is null)
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.GetUsersInRoleAsync(roleName) as List<User>;

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new UserProblemException($"Не найден пользователь с id {id}");

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> LoginUserAsync(LoginUserDTO dto)
        {
            User? user = null;
            if (dto.Email is not null)
                user = await _userManager.FindByEmailAsync(dto.Email);
            else if (dto.UserName is not null)
                user = await _userManager.FindByNameAsync(dto.UserName);

            if(user is null)
                throw new NotFoundException($"Не найден пользватель с {dto.Email}");
            
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);

            return result.Succeeded;
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
