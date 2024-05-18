using Domain.Enums;

namespace Domain.Entities

{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
