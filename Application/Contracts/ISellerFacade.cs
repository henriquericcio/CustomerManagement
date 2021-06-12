using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Contracts
{
    public interface ISellerFacade
    {
        Task<IEnumerable<dynamic>> Get();
    }
}