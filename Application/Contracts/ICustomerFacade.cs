using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Model;

namespace CustomerManagement.Application
{
    public interface ICustomerFacade
    {
        Task<IEnumerable<dynamic>> Get(
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