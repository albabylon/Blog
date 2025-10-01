using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Identity;
using Blog.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<bool> UpdateUserRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId) 
                ?? throw new UserProblemException($"Не найден пользователь с {userId}");
  
            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
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

            user.Email = dto.Email;
            user.Nickname = dto.Nickname;
            user.UserName = dto.Nickname;
            user.Bio = dto.Bio;
            user.ProfilePictureUrl = dto.ProfilePictureUrl;

            if (!string.IsNullOrEmpty(dto.PasswordNew))
                await _userManager.ChangePasswordAsync(user, dto.PasswordOld, dto.PasswordNew);

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<UserDTO>(user);
            result.Role = await _userManager.GetRolesAsync(user);

            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? roleName = null)
        {
            List<User>? users;
            if (roleName is null)
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.GetUsersInRoleAsync(roleName) as List<User>;

            var result = _mapper.Map<IEnumerable<UserDTO>>(users);

            foreach (var userDto in result)
            {
                var user = users.FirstOrDefault(u => u.Id == userDto.Id);
                if (user != null)
                {
                    userDto.Role = await _userManager.GetRolesAsync(user);
                }
            }

            return result;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new UserProblemException($"Не найден пользователь с id {id}");

            var result = _mapper.Map<UserDTO>(user);
            result.Role = await _userManager.GetRolesAsync(user);
            result.CreatedAt = user.CreatedAt;

            return result;
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserProblemException($"Не найден пользователь {email}");

            var result = _mapper.Map<UserDTO>(user);
            result.Role = await _userManager.GetRolesAsync(user);
            result.CreatedAt = user.CreatedAt;

            return result;
        }

        public async Task<bool> LoginUserAsync(LoginUserDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email) ??
                throw new NotFoundException($"Не найден пользователь с {dto.Email}");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);

            return result.Succeeded;
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task AddCustomClaimsAsync(string id, IEnumerable<Claim> claims)
        {
            var user = await _userManager.FindByIdAsync(id) ??
                throw new NotFoundException($"Не найден пользватель {id}");

            await _userManager.AddClaimsAsync(user, claims);

            // Обновляем claims в cookie, если пользователь сейчас авторизован
            await _signInManager.RefreshSignInAsync(user);
        }


        public bool IsLogged(ClaimsPrincipal claims)
        {
            return _signInManager.IsSignedIn(claims);
        }

        public async Task<bool> HasPriorityRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) 
                ?? throw new UserProblemException($"Не найден пользователь с {userId}");
            
            var roles = await _userManager.GetRolesAsync(user);
            
            return roles.Any(x => x == SystemRoles.Admin || x == SystemRoles.Moderator);
        }
    }
}
