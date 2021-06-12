using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public class SessionDto
    { 
        public Guid Id { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
    }
}