using System;
using CustomerManagement.Model;

namespace CustomerManagement.Application
{
    public interface ISessionFacade
    {
        Session GetSession(Guid sessionKey);
    }
}