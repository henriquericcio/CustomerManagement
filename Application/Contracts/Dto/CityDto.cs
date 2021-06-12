using System;

namespace CustomerManagement.Application.Contracts.Dto
{
    public sealed class CityDto
    {
        public CityDto(Guid id, string name)
        {
            Id = id;
            Name = name;

        }
        public Guid Id { get;   }
        public string Name { get;   }
    }
}