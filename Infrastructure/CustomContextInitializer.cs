using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Model;

namespace CustomerManagement.Infrastructure
{
    public static class CustomContextInitializer
    {
        public static async void Seed(CustomerManagementContext context)
        {
            if (context.Users.Any()) return;

            var sellers = await SeedUsers();
            var regions = await SeedRegions();
            await SeedCustomers();

            await context.SaveChangesAsync();

            async Task<dynamic> SeedUsers()
            {
                var sellerOne = new User {Email = "seller1@app.com", Password = "seller@1", Role = Role.Seller};
                var sellerTwo = new User {Email = "seller2@app.com", Password = "seller@2", Role = Role.Seller};
                
                await context.Users.AddRangeAsync(
                    new User {Email = "admin@app.com", Password = "admin@123", Role = Role.Administrator},
                    sellerOne,
                    sellerTwo);
                
                return new {One=sellerOne,Two=sellerTwo};
            }
            
            async Task<dynamic> SeedRegions()
            {
                var south = new Region("South");
                south.Cities.Add(new City("Porto Alegre"));
                south.Cities.Add(new City("Curitiba"));

                var north = new Region("North");
                north.Cities.Add(new City("Manaus"));
                north.Cities.Add(new City("Belem"));

                var east = new Region("East");
                east.Cities.Add(new City("Recife"));
                east.Cities.Add(new City("Joao Pessoa"));

                var west = new Region("West");
                west.Cities.Add(new City("Rio Branco"));
                west.Cities.Add(new City("Goiania"));

                await context.Regions.AddRangeAsync(south, north, east, west);
                return new {South=south,North=north,East=east,West=west};
            }

            async Task SeedCustomers()
            {
                await context.Customers.AddRangeAsync(
                    new Customer
                    {
                        City = regions.South.Cities[0],
                        Region = regions.South,
                        Classification = Classification.Regular,
                        Gender = Gender.Male,
                        Name = "Customer One",
                        LastPurchase = new DateTime(2010, 01, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[1],
                        Region = regions.South,
                        Classification = Classification.Sporadic,
                        Gender = Gender.Female,
                        Name = "Customer Two",
                        LastPurchase = new DateTime(2010, 02, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[0],
                        Region = regions.South,
                        Classification = Classification.Vip,
                        Gender = Gender.Male,
                        Name = "Customer Three",
                        LastPurchase = new DateTime(2010, 03, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[1],
                        Region = regions.South,
                        Classification = Classification.Regular,
                        Gender = Gender.Female,
                        Name = "Customer Four",
                        LastPurchase = new DateTime(2010, 04, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[0],
                        Region = regions.South,
                        Classification = Classification.Sporadic,
                        Gender = Gender.Male,
                        Name = "Customer Five",
                        LastPurchase = new DateTime(2010, 05, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[1],
                        Region = regions.South,
                        Classification = Classification.Vip,
                        Gender = Gender.Female,
                        Name = "Customer Six",
                        LastPurchase = new DateTime(2010, 06, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.South.Cities[0],
                        Region = regions.South,
                        Classification = Classification.Regular,
                        Gender = Gender.Male,
                        Name = "Customer Seven",
                        LastPurchase = new DateTime(2010, 07, 01),
                        Seller = sellers.One
                    },
                    new Customer
                    {
                        City = regions.North.Cities[0],
                        Region = regions.North,
                        Classification = Classification.Sporadic,
                        Gender = Gender.Female,
                        Name = "Customer Eight",
                        LastPurchase = new DateTime(2010, 08, 01),
                        Seller = sellers.Two
                    },
                    new Customer
                    {
                        City = regions.East.Cities[0],
                        Region = regions.East,
                        Classification = Classification.Vip,
                        Gender = Gender.Male,
                        Name = "Customer Nine",
                        LastPurchase = new DateTime(2010, 09, 01),
                        Seller = sellers.Two
                    },
                    new Customer
                    {
                        City = regions.West.Cities[0],
                        Region = regions.West,
                        Classification = Classification.Regular,
                        Gender = Gender.Female,
                        Name = "Customer Ten",
                        LastPurchase = new DateTime(2010, 10, 01),
                        Seller = sellers.Two
                    }
                );
            }
        }
    }
}