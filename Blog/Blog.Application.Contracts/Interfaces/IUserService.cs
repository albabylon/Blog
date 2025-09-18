using Blog.DTOs.User;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> CreateUserAsync(CreateUserDTO dto);
        Task<bool> LoginUserAsync(LoginUserDTO dto);
        Task LogoutUserAsync();
        Task EditUserAsync(EditUserDTO dto);
        Task<bool> DeleteUserAsync(string id);
        Task<UserDTO> GetUserAsync(string id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
