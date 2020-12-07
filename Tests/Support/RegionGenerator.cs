using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Model;

namespace CustomerManagement.Tests.Support
{
    public static class RegionGenerator
    {
        public static IList<Region> Generate(int amountRegions, int amountCitiesPerRegion)
        {
            if (amountRegions < 1 || amountCitiesPerRegion < 1)
                throw new ArgumentException("amountRegions or amountCitiesPerRegions must be greater than zero");

            if (amountRegions > 9999 || amountCitiesPerRegion > 9999)
                throw new ArgumentException("amountRegions or amountCitiesPerRegions limited to 9999");
            
            var regions = new List<Region>();

            for (var i = 0; i < amountRegions; i++)
            {
                var region = new Region($"R{i:0000}");
                regions.Add(region);
                
                for (var j = 0; j < amountCitiesPerRegion; j++)
                {
                    region.Cities.Add(new City($"C{i:0000}|{j:0000}"));
                }
            }

            return regions.OrderBy(r=>r.Id).ToList();
        }
    }
}