using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;

namespace NetChallenge.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private IList<Location> Locations { get; set; } = new List<Location>();

        public IEnumerable<Location> AsEnumerable()
        {
            return Locations;
        }

        public void Add(Location item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new LocationWithoutNameException();
            } else if (Locations.Any(p => p.Name == item.Name))
            {
                throw new ExistingLocationNameException(item.Name);
            } else if (string.IsNullOrEmpty(item.Neighborhood))
            {
                throw new LocationWithoutNeighborhoodException();
            }
            Locations.Add(item);
        }
    }
}