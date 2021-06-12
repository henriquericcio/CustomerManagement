using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts.Dto;

namespace CustomerManagement.Application.Contracts
{
    public interface ISellerFacade
    {
        Task<IEnumerable<SellerDto>> Get();
    }
}