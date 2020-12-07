using System;

namespace CustomerManagement.Model
{
    public class Session
    {
        protected Session(){}
        public Session(string email, string password)
        {
            Email = email;
            Password = password;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}