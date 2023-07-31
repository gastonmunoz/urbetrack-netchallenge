using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Office
    {
        public Location Location { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public string[] AvailableResources { get; set; }
        public IList<Booking> Bookings { get; set; } = new List<Booking>();

        public bool HasThisResources(IEnumerable<string> resourcesNeeded)
        {
            bool isValid = true;
            foreach (string resource in resourcesNeeded)
            {
                isValid = AvailableResources.Contains(resource);
                if (!isValid)
                {
                    break;
                }
            };
            return isValid;
        }
    }
}