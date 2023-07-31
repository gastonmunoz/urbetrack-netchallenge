using System.Collections.Generic;

namespace NetChallenge.Domain
{
    /// <summary>
    /// Es un local que puede contener varias oficinas
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Nombre del local
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Barrio donde se encuentra el local
        /// </summary>
        public string Neighborhood { get; set; }

        /// <summary>
        /// Listado de oficinas del local
        /// </summary>
        public IList<Office> Offices { get; set; } = new List<Office>();

        public Location(string name, string neighborhood)
        {
            Name = name;
            Neighborhood = neighborhood;
        }
    }
}