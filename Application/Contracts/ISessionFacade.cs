using System;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts.Dto;

namespace CustomerManagement.Application.Contracts
{
    public interface ISessionFacade
    {
        Task<SessionDto> GetSessionAsync(Guid sessionKey);
        Task Save(SessionDto session);
        Task Delete(SessionDto session);
    }
}