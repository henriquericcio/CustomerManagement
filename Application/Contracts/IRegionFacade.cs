using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Model;

namespace CustomerManagement.Application.Contracts
{
    public interface IRegionFacade
    {
        Task<IEnumerable<Region>> Get();
    }
}