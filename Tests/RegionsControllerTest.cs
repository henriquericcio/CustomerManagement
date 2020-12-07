using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerManagement.Controllers;
using CustomerManagement.Model;
using CustomerManagement.Tests.Support;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CustomerManagement.Tests
{
    public class RegionsControllerTest
    {
        private readonly FakeContextFactory _factory = new FakeContextFactory();

        [Fact]
        public async Task it_should_get_all_regions()
        {
            //create region data
            var regions = RegionGenerator.Generate(4,3);

            //create json representation in order to assert
            var expected = JsonSerializer.Serialize(regions);

            //include into database
            await using var context = await _factory.CreateContextAsync();
            await context.AddRangeAsync(regions);
            await context.SaveChangesAsync();

            //sut
            var controller = new RegionsController(new Mock<ILogger<RegionsController>>().Object, context);

            //execute
            var result = await controller.Get();
            var actual = result.Value as IList<Region>;

            //assertions
            Assert.NotNull(actual);
            //using json representation to compare since object reference are the same 
            Assert.Equal(expected, JsonSerializer.Serialize(actual));
        }
    }
}