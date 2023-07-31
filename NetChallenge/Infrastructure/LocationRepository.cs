using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;

namespace NetChallenge.Infrastructure
{
    /// <summary>
    /// Repositorio de locales (Location)
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private IList<Location> Locations { get; set; } = new List<Location>();

        /// <summary>
        /// Devuelve todos los locales
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Location> AsEnumerable()
        {
            return Locations;
        }

        /// <summary>
        /// Agrega un local nuevo
        /// </summary>
        /// <param name="item">Local a agregar</param>
        /// <exception cref="LocationWithoutNameException"></exception>
        /// <exception cref="ExistingLocationNameException"></exception>
        /// <exception cref="LocationWithoutNeighborhoodException"></exception>
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