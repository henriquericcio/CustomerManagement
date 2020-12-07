using System.Collections.Generic;
using CustomerManagement.Model;

namespace CustomerManagement.Tests.Support
{
    public static class UserGenerator
    {
        public static IList<User> Generate()
        {
            return new List<User>
            {
                new User {Email = "admin@app.com", Password = "admin@123", Role = Role.Administrator},
                new User {Email = "seller1@app.com", Password = "seller@1", Role = Role.Seller},
                new User {Email = "seller2@app.com", Password = "seller@2", Role = Role.Seller}
            };
        }
    }
}