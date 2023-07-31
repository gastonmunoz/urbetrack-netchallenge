using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;

namespace NetChallenge.Infrastructure
{
    /// <summary>
    /// Repositorio de oficinas (Office)
    /// </summary>
    public class OfficeRepository : IOfficeRepository
    {
        private IList<Office> Offices { get; set; } = new List<Office>();

        /// <summary>
        /// Devuelve todas las oficinas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Office> AsEnumerable()
        {
            return Offices;
        }

        /// <summary>
        /// Agrega una oficina nueva
        /// </summary>
        /// <param name="item">Oficina a agregar</param>
        /// <exception cref="InvalidLocationNameException"></exception>
        /// <exception cref="OfficeWithoutNameException"></exception>
        /// <exception cref="ExistingOfficeNameInLocationException"></exception>
        /// <exception cref="InvalidMaxCapacityException"></exception>
        public void Add(Office item)
        {
            if (item.Location == null)
            {
                throw new InvalidLocationNameException();
            } else if (string.IsNullOrEmpty(item.Name))
            {
                throw new OfficeWithoutNameException();
            } else if (item.Location.Offices.Any(p => p.Name == item.Name))
            {
                throw new ExistingOfficeNameInLocationException(item.Name, item.Location.Name);
            } else if (item.MaxCapacity <= 0)
            {
                throw new InvalidMaxCapacityException();
            }
            Offices.Add(item);
            item.Location.Offices.Add(item);
        }
    }
}