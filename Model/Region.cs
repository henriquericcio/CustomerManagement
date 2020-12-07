using System;
using System.Collections.Generic;

namespace CustomerManagement.Model
{
    public class Region
    {
        public Region(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cities = new List<City>();
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public virtual IList<City> Cities { get; protected set; }
    }
}