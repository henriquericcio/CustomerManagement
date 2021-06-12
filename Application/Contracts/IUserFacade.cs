using CustomerManagement.Model;

namespace CustomerManagement.Controllers
{
    public interface IUserFacade
    {
        User GetByEmail(string sessionEmail);
    }
}