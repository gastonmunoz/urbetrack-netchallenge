using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    /// <summary>
    /// Es una oficina dentro de un local
    /// </summary>
    public class Office
    {
        /// <summary>
        /// Local a donde pertenece la oficina
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Nombre de la oficina
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Capacidad máxima de la oficina
        /// </summary>
        public int MaxCapacity { get; set; }

        /// <summary>
        /// Recursos disponibles en la oficina
        /// </summary>
        public string[] AvailableResources { get; set; }

        /// <summary>
        /// Reuniones programadas.
        /// </summary>
        public IList<Booking> Bookings { get; set; } = new List<Booking>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Nombre de la oficina</param>
        /// <param name="maxCapacity">Capacidad de la oficina</param>
        public Office(string name, int maxCapacity)
        {
            Name = name;
            MaxCapacity = maxCapacity;
        }

        /// <summary>
        /// Indica si una oficina tiene los recursos solicitados.
        /// </summary>
        /// <param name="resourcesNeeded">Recursos solicitados</param>
        /// <returns></returns>
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