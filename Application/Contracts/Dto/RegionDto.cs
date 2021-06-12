using System;
using System.Collections.Generic;

namespace CustomerManagement.Application.Contracts.Dto
{
    public class RegionDto
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public virtual IEnumerable<CityDto> Cities { get;  set; }
    }
}