using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class UserDto
    {
        public UserDto(Guid id, string role, string email, string password)
        {
            Id = id;
            Role = role;
            Email = email;
            Password = password;
        }
        
        public Guid Id { get;   }
        public string Role { get;  }
        public string Email { get;  }
        public string Password { get;  }
    }
}