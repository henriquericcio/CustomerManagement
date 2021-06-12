using System;
using System.Linq;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;

namespace CustomerManagement.Application
{
    public class SessionFacade : ISessionFacade
    {
        private readonly CustomerManagementContext _context;
        
        public SessionFacade(CustomerManagementContext context)
        {
            _context = context;
        }
        public Session GetSession(Guid sessionKey)
        {
            return _context.Sessions.FirstOrDefault(s => s.Id == sessionKey);
        }
    }
}