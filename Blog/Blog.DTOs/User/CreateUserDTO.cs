namespace Blog.DTOs.User
{
    public class CreateUserDTO
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
