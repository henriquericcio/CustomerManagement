using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class CustomersDto
    {
        public Guid Id{ get; set; }
        public string Classification { get; set; }
        public string Name{ get; set; }
        public string Phone{ get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Seller { get; set; }
        public DateTime LastPurchase { get; set; }
    }
}