using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts.Dto;

namespace CustomerManagement.Application.Contracts
{
    public interface IRegionFacade
    {
        Task<IEnumerable<RegionDto>> Get();
    }
}