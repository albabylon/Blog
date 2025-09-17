using Blog.DTOs.User;

namespace Blog.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(CreateUserDTO dto);
        Task EditUserAsync(EditUserDTO dto);
        Task DeleteUserAsync(Guid id);
        Task<UserDTO> GetUserAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
