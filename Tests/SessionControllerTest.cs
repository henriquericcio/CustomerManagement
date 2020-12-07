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

    public class SessionsControllerTest
    {
        private readonly FakeContextFactory _factory = new FakeContextFactory();

        public SessionsControllerTest()
        {
            //create data
            var users = UserGenerator.Generate(); 

            //include into database
            using var task = _factory.CreateContextAsync();
            task.Result.AddRangeAsync(users);
            task.Result.SaveChangesAsync();
        }

        [Fact]
        public async Task it_should_generate_error_given_invalid_credentials()
        {
            //invalid credentials
            var expected = new Session("email", "password");

            //sut
            await using var context = await _factory.CreateContextAsync();
            var controller = new SessionsController(new Mock<ILogger<SessionsController>>().Object, context);

            //
            var result = await controller.Post(expected);

            //
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task it_should_create_session_given_valid_seller_credentials()
        {
            await using var context = await _factory.CreateContextAsync();

            //valid credentials for seller
            var user = context.Users.First(u => u.Role == Role.Seller);
            var expected = new Session(user.Email, user.Password);

            //sut
            var controller = new SessionsController(new Mock<ILogger<SessionsController>>().Object, context);

            //execute
            var result = await controller.Post(expected);

            //assert
            Assert.IsType<CreatedAtActionResult>(result);

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);

            dynamic actual = createdAtActionResult.Value;
            Assert.NotNull(actual);
            Assert.Equal(expected.Email, actual.Email);
        }

        [Fact]
        public async Task it_should_create_session_given_valid_admin_credentials()
        {
            await using var context = await _factory.CreateContextAsync();

            //valid credentials for administrator
            var user = context.Users.First(u => u.Role == Role.Administrator);
            var expected = new Session(user.Email, user.Password);

            //sut
            var controller = new SessionsController(new Mock<ILogger<SessionsController>>().Object, context);

            //execute
            var result = await controller.Post(expected);

            //assert
            Assert.IsType<CreatedAtActionResult>(result);

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);

            dynamic actual = createdAtActionResult.Value;
            Assert.NotNull(actual);
            Assert.Equal(expected.Email, actual.Email);
        }

        [Fact]
        public async Task it_should_delete_session_given_valid_id()
        {
            await using var context = await _factory.CreateContextAsync();

            //valid credentials for administrator
            var user = context.Users.First();
            var expected = new Session(user.Email, user.Password);

            //sut
            var controller = new SessionsController(new Mock<ILogger<SessionsController>>().Object, context);
            
            //execute
            var sessionId = ((dynamic) ((CreatedAtActionResult) await controller.Post(expected)).Value).Id;
            var result = await controller.Delete(sessionId);

            //assert
            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Sessions);
        }
    }
}