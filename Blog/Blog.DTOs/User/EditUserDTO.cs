namespace Blog.DTOs.User
{
    public class EditUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string PasswordNew { get; set; }
        public IList<string> Role { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
