using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application
{
    public class UserFacade : IUserFacade
    {
        private readonly CustomerManagementContext _context;

        public UserFacade(CustomerManagementContext context)
        {
            _context = context;
        }
        public async Task<UserDto> GetByEmail(string email)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : new UserDto(user.Id, user.Role.ToString(), user.Email, user.Password);
        }
        public UserDto GetByEmailPassword(string email, string password)
        {
            var user  =  _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user == null ? null : new UserDto(user.Id, user.Role.ToString(), user.Email, user.Password);
        }
    }
}