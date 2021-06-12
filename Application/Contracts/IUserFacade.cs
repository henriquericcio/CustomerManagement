using CustomerManagement.Application.Contracts.Dto;

namespace CustomerManagement.Application.Contracts
{
    public interface IUserFacade
    {
        UserDto GetByEmail(string sessionEmail);
        UserDto GetByEmailPassword(string email, string password);
    }
}