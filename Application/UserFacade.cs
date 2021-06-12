using System;
using CustomerManagement.Model;

namespace CustomerManagement.Controllers
{
    class UserFacade : IUserFacade
    {
        public User GetByEmail(string sessionEmail)
        {
            //var user = _context.Users.FirstOrDefault(u => u.Email == session.Email);
            throw new NotImplementedException();
        }
    }
}