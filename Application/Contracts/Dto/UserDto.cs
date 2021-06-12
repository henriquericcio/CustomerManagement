using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public class UserDto
    {
        public Guid Id { get;  set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}