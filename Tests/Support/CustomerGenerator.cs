using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Model;

namespace CustomerManagement.Tests.Support
{
    public class CustomerGenerator
    {
        private readonly IList<Region> _regions;
        private readonly IList<User> _users;
        private readonly DateTime _since;
        private readonly DateTime _to;
        private readonly Random _random = new Random();

        public CustomerGenerator(IList<Region> regions, IList<User> users, DateTime since, DateTime to)
        {
            _regions = regions;
            _users = users;
            _since = since;
            _to = to;
        }
        public IList<Customer> Generate(int amount)
        {
            if (amount < 1 )
                throw new ArgumentException("amount must be greater than zero");

            if (amount > 9999 )
                throw new ArgumentException("amount limited to 9999");
            
            var customers = new List<Customer>();
            
            for (var i = 0; i < amount; i++)
            {
                var selectedRegion = GetRandomRegion();

                customers.Add(
                    new Customer
                    {
                        //Id = id,
                        Name = $"C{i:0000}",
                        Region = selectedRegion,
                        City = GetRandomCity(selectedRegion),
                        Classification = GetRandomClassification(),
                        Gender = GetRandomGender(),
                        Phone = GetRandomPhone(),
                        LastPurchase = GetRandomPurchaseDate(),
                        Seller = GetRandomSeller()
                    });    
            }

            return customers;
        }

        private Region GetRandomRegion()
        {
            return _regions[_random.Next(_regions.Count)];
        }

        private User GetRandomSeller()
        {
            var sellers = _users.Where(u => u.Role == Role.Seller).ToList();
            return sellers[_random.Next(sellers.Count)];
        }

        private City GetRandomCity(Region region)
        {
            return region.Cities[_random.Next(region.Cities.Count)];
        }

        private Classification GetRandomClassification()
        {
            var values = Enum.GetValues(typeof(Classification));
            var a = values.GetLength(0);
            var b =  values.GetValue(_random.Next(a));
            var c = b as Classification? ?? Classification.Vip;
            return c;
        }

        private Gender GetRandomGender()
        {
            var values = Enum.GetValues(typeof(Gender));
            var a = values.GetLength(0);
            var b =  values.GetValue(_random.Next(a));
            var c = b as Gender? ?? Gender.Female;
            return c;
        }
        
        private string GetRandomPhone()
        {
            var pre = _random.Next(9999);
            var post = _random.Next(9999);
            return $"{pre:0000}-{post:0000}";
        }
        
        private DateTime GetRandomPurchaseDate()
        {
            var days = (int) Math.Floor(_to.Subtract(_since).TotalDays);
            var asd = _random.Next(days);
            return _since.AddDays(asd);
        }
        
    }
}