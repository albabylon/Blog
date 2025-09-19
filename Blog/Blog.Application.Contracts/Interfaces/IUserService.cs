using Blog.DTOs.User;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> CreateUserAsync(CreateUserDTO dto);
        Task<bool> LoginUserAsync(LoginUserDTO dto);
        Task LogoutUserAsync();
        Task<UserDTO> EditUserAsync(EditUserDTO dto, string id);
        Task<bool> DeleteUserAsync(string id);
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? roleName = null);
    }
}
