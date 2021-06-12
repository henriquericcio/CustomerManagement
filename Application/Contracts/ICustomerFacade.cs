using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts.Dto;

namespace CustomerManagement.Application.Contracts
{
    public interface ICustomerFacade
    {
        Task<IEnumerable<CustomersDto>> Get(
            string name,
            Gender? gender,
            Guid? city,
            Guid? region,
            Classification? classification,
            DateTime? startDate,
            DateTime? endDate,
            Guid? seller,
            User user);
    }
}
