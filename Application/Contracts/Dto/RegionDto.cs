using System;
using System.Collections.Generic;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class RegionDto
    {
        public RegionDto(Guid id, string name, IEnumerable<CityDto> cities)
        {
            Id = id;
            Name = name;
            Cities = cities;

        }
        public Guid Id { get;   }
        public string Name { get;   }
        public IEnumerable<CityDto> Cities { get;  }
    }
}