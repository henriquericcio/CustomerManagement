using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class SellerDto
    {
        public SellerDto(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
        public Guid Id{ get; }
        public string Email { get; }
    }
}