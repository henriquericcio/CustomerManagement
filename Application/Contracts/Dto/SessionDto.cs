using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class SessionDto
    {
        public Guid Id { get; set; }
        public string Email { get; set;}
        public string Password { get; set;}
        public string Role { get; set; }
    }
}