using System;

namespace CustomerManagement.Model
{
    public class Customer
    {
        public Customer() => Id = Guid.NewGuid();
        public Guid Id { get; private set; }
        public Classification Classification { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public virtual City City { get; set; }
        public virtual Region Region { get; set; }
        public virtual User Seller { get; set; }
        public DateTime LastPurchase { get; set; }
    }
}