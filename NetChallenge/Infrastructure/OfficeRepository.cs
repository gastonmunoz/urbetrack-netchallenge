using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;

namespace NetChallenge.Infrastructure
{
    public class OfficeRepository : IOfficeRepository
    {
        public IList<Office> Offices { get; set; } = new List<Office>();

        public IEnumerable<Office> AsEnumerable()
        {
            return Offices;
        }

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