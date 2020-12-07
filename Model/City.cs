using System;

namespace CustomerManagement.Model
{
    public class City
    {
        public City(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}