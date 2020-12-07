using System;

namespace CustomerManagement.Model
{
    public class User
    {
        public User() => Id = Guid.NewGuid();
        public Guid Id { get; private set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}