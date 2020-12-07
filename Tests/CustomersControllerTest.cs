using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Controllers;
using CustomerManagement.Model;
using CustomerManagement.Tests.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CustomerManagement.Tests
{
    public class CustomersControllerTest
    {
        private readonly FakeContextFactory _factory = new FakeContextFactory();
        private readonly IList<Region> _regions;
        private readonly IList<Customer> _customers;
        private readonly Guid _adminSessionId;
        private readonly Guid _sellerOneSessionId;
        private readonly User _userSellerOne;

        public CustomersControllerTest()
        {
            //create data
            var users = UserGenerator.Generate();
            _regions = RegionGenerator.Generate(4, 3);
            _customers = new CustomerGenerator(_regions, users, new DateTime(2010, 01, 01), new DateTime(2010, 12, 31))
                .Generate(100).OrderBy(c => c.Id).ToList();

            //include into database
            using var task = _factory.CreateContextAsync();
            var context = task.Result;
            context.AddRangeAsync(users);
            context.AddRangeAsync(_customers);
            context.SaveChangesAsync();

            //retrieve users
            var userAdm = task.Result.Users.First(u => u.Role == Role.Administrator);
            _userSellerOne = task.Result.Users.First(u => u.Role == Role.Seller && u.Email.Contains("1"));
            
            //create sessions for each user
            var controller = new SessionsController(new Mock<ILogger<SessionsController>>().Object, context);
            _adminSessionId = ((dynamic) ((CreatedAtActionResult) controller.Post(new Session(userAdm.Email,userAdm.Password)).Result).Value).Id;
            _sellerOneSessionId = ((dynamic) ((CreatedAtActionResult) controller.Post(new Session(_userSellerOne.Email,_userSellerOne.Password)).Result).Value).Id;
        }

        [Fact]
        public async Task it_should_get_all_customers()
        {
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers);

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }


        [Fact]
        public async Task it_should_get_only_male_customers()
        {
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Gender == Gender.Male));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, Gender.Male, null, null, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }

        [Fact]
        public async Task it_should_get_only_female_customers()
        {
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Gender == Gender.Female));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, Gender.Female, null, null, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }        
        
        [Fact]
        public async Task it_should_get_only_customers_with_name_starting_with_a_given_value()
        {
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Name.StartsWith("C001")));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get("C001", null, null, null, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }        
        
        [Fact]
        public async Task it_should_get_only_customers_from_a_given_city()
        {
            //select one city
            var selectedCityId = _regions[0].Cities[0].Id;
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.City.Id ==selectedCityId));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, selectedCityId, null, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }        

        [Fact]
        public async Task it_should_get_only_customers_from_a_given_region()
        {
            //select one region
            var selectedRegionId = _regions[0].Id;
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Region.Id ==selectedRegionId));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, selectedRegionId, null, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }       
        
        [Fact]
        public async Task it_should_get_only_customers_from_a_given_classification()
        {
            //select one classification
            const Classification selectedClassification = Classification.Regular;
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Classification ==selectedClassification));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,selectedClassification, null, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }       

        [Fact]
        public async Task it_should_get_only_customers_from_a_given_startDate()
        {
            //select startDate
            var selectedStartDate = new DateTime(2010,04,01);
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.LastPurchase >=selectedStartDate));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, selectedStartDate, null, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }       

        [Fact]
        public async Task it_should_get_only_customers_from_a_given_endDate()
        {
            //select endDate
            var selectedEndDate = new DateTime(2010,08,25);
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.LastPurchase <=selectedEndDate));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, null, selectedEndDate, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }       

        [Fact]
        public async Task it_should_get_only_customers_from_a_given_period()
        {
            //select period
            var selectedStartDate = new DateTime(2010,04,01);
            var selectedEndDate = new DateTime(2010,08,25);
            
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.LastPurchase>=selectedStartDate && c.LastPurchase <=selectedEndDate));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, selectedStartDate, selectedEndDate, null,_adminSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }       

        
        [Fact]
        public async Task it_should_get_only_customers_from_a_given_seller()
        {
            //create simplified representation in order to assert
            var expected = SimplifyCustomer(_customers.Where(c => c.Seller== _userSellerOne));

            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, null, null, null,_sellerOneSessionId);
            var actual = result.Value;

            //assertions
            Assert.NotNull(actual);
            Assert.Equal(expected, SimplifyCustomer(actual));
        }   

        [Fact]
        public async Task it_should_return_unauthorized_given_invalid_sessionId()
        {
            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, null, null, null,Guid.NewGuid());

            //assertions
            Assert.IsType<UnauthorizedResult>(result.Result);
        }   
        
        [Fact]
        public async Task it_should_return_unauthorized_given_no_sessionId()
        {
            //sut
            using var context = _factory.CreateContextAsync();
            var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, await context);

            //execute
            var result = await controller.Get(null, null, null, null,null, null, null, null,null);

            //assertions
            Assert.IsType<UnauthorizedResult>(result.Result);
        }   
 
        
        private static IEnumerable<dynamic> SimplifyCustomer(IEnumerable<dynamic> enumerable)
        {
            return enumerable
                .OrderBy(c => c.Id)
                .ThenBy(c => c.City)
                .ThenBy(c => c.Classification)
                .ThenBy(c => c.Gender)
                .ThenBy(c => c.LastPurchase)
                .ThenBy(c => c.Name)
                .ThenBy(c => c.Phone)
                .ThenBy(c => c.Region)
                .ThenBy(c => c.Seller)
                .ToList();
        }
        private static IEnumerable<dynamic> SimplifyCustomer(IEnumerable<Customer> enumerable)
        {
            // Phone = 7249-4246, Gender = Female, City = C0002|0001, Region = R0002, Seller = seller1@app.com, LastPurchase = 10/19/2010 12:00:00 AM }
            return enumerable
                .Select(c => new
                {
                    c.Id,Classification= c.Classification.ToString(), c.Name, c.Phone, Gender = c.Gender.ToString(),City = c.City.Name,
                    Region = c.Region.Name , Seller = c.Seller.Email, c.LastPurchase, 
                })
                .OrderBy(c => c.Id)
                .ThenBy(c => c.City)
                .ThenBy(c => c.Classification)
                .ThenBy(c => c.Gender)
                .ThenBy(c => c.LastPurchase)
                .ThenBy(c => c.Name)
                .ThenBy(c => c.Phone)
                .ThenBy(c => c.Region)
                .ThenBy(c => c.Seller)
                .ToList();
        }
    }
}