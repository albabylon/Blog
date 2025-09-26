using Blog.DTOs.User;
using System.Security.Claims;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> CreateUserAsync(CreateUserDTO dto);
        Task<bool> UpdateUserRoleAsync(string userId, string role);
        Task<bool> LoginUserAsync(LoginUserDTO dto);
        Task LogoutUserAsync();
        Task<UserDTO> EditUserAsync(EditUserDTO dto, string id);
        Task<bool> DeleteUserAsync(string id);
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? roleName = null);
        Task AddCustomClaimsAsync(string id, IEnumerable<Claim> claims);

        bool IsLogged(ClaimsPrincipal claims);
    }
}
