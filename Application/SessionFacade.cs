using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Application.Contracts;
using CustomerManagement.Application.Contracts.Dto;
using CustomerManagement.Infrastructure;
using CustomerManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application
{
    public class SessionFacade : ISessionFacade
    {
        private readonly CustomerManagementContext _context;

        public SessionFacade(CustomerManagementContext context)
        {
            _context = context;
        }

        public async Task<SessionDto> GetSessionAsync(Guid sessionKey)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionKey);
            return session == null
                ? null
                : new SessionDto
                {
                    Id = session.Id,
                    Email = session.Email,
                    Password = session.Password
                };
        }

        public async Task Save(SessionDto dto)
        {
            var session = new Session(dto.Email, dto.Password);
            _context.Entry(session).State = EntityState.Added;
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SessionDto dto)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == dto.Id);
            if (session == null) return;

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}