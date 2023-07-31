using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;

namespace NetChallenge
{
    /// <summary>
    /// Servicio de alquiler de oficinas
    /// </summary>
    public class OfficeRentalService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IBookingRepository _bookingRepository;

        public OfficeRentalService(ILocationRepository locationRepository, IOfficeRepository officeRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
            _bookingRepository = bookingRepository;
        }

        /// <summary>
        /// Agregar un local nuevo.
        /// </summary>
        /// <param name="request"></param>
        public void AddLocation(AddLocationRequest request)
        {
            Location location = new Location(request.Name, request.Neighborhood);
            _locationRepository.Add(location);
        }

        /// <summary>
        /// Agregar una oficina a un local.
        /// </summary>
        /// <param name="request"></param>
        public void AddOffice(AddOfficeRequest request)
        {
            Location location = _locationRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.LocationName);

            Office office = new Office(request.Name, request.MaxCapacity)
            {
                AvailableResources = request.AvailableResources?.ToArray(),
                Location = location
            };
            _officeRepository.Add(office);
        }

        /// <summary>
        /// Reservar una oficina.
        /// </summary>
        /// <param name="request"></param>
        public void BookOffice(BookOfficeRequest request)
        {
            Location location = _locationRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.LocationName);
            Office office = _officeRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.OfficeName);

            Booking booking = new Booking(request.UserName)
            {
                DateTime = request.DateTime,
                Duration = request.Duration,
                Office = office,
                Location = location
            };

            _bookingRepository.Add(booking);
        }

        /// <summary>
        /// Obtener un listado de reservas de una oficina.
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="officeName"></param>
        /// <returns></returns>
        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            return _bookingRepository.AsEnumerable().Where(p => p.Location.Name == locationName && p.Office.Name == officeName)
                .Select(p => new BookingDto()
                {
                    UserName = p.UserName,
                    DateTime = p.DateTime,
                    Duration = p.Duration,
                    OfficeName = p.Office.Name,
                    LocationName = p.Location.Name
                });
        }

        /// <summary>
        /// Obtener el listado de locales.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LocationDto> GetLocations()
        {
            return _locationRepository.AsEnumerable().Select(p => new LocationDto()
            {
                Name = p.Name,
                Neighborhood = p.Neighborhood,
            });
        }

        /// <summary>
        /// Obtener todas las oficinas de un local.
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns></returns>
        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            return _officeRepository.AsEnumerable().Where(p => p.Location.Name == locationName).Select(p => new OfficeDto()
            {
                Name = p.Name,
                AvailableResources = p.AvailableResources,
                LocationName = p.Location.Name,
                MaxCapacity = p.MaxCapacity
            });
        }

        /// <summary>
        /// Obtener un listado de oficinas que coincidan con las especificaciones, ordenados por conveniencia.
        /// Las sugerencias tiene que cumplir estas condiciones:
        /// - permitir la capacidad necesaria
        /// - tener todos los recursos solicitados
        /// - es preferible reservar una oficina en el barrio solicitado pero si no hay ninguna se pueden sugerir otros locales
        /// - es preferible mantener libres las oficinas mas grandes
        /// - es preferible mantener libres las oficinas con mas recursos de los que se requieren
        /// 
        /// Las sugerencias deben devolver todas las oficinas que cumplan las condiciones.
        /// Las preferencias deben establecer el orden de los resultado, siendo la primera la que mayor coincidencia tiene con la consulta.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            IList<Office> offices = _officeRepository.AsEnumerable().Where(p => p.MaxCapacity >= request.CapacityNeeded && p.HasThisResources(request.ResourcesNeeded)).ToList();
            IOrderedEnumerable<Office> orderedOfficesByCapacity = offices.OrderBy(p => p.MaxCapacity);
            IOrderedEnumerable<Office> orderedOfficesByResources = orderedOfficesByCapacity.OrderBy(p => p.AvailableResources.Count());
            List<Office> officesInNeighborhood = orderedOfficesByResources.ToList();
            
            if (!string.IsNullOrWhiteSpace(request.PreferedNeigborHood))
            {
                officesInNeighborhood = orderedOfficesByResources.Where(p => p.Location.Neighborhood == request.PreferedNeigborHood).ToList();
                officesInNeighborhood.AddRange(offices.Where(p => !officesInNeighborhood.Contains(p)));
            }

            return officesInNeighborhood.Select(p => new OfficeDto()
            {
                AvailableResources = p.AvailableResources,
                LocationName = p.Location.Name,
                MaxCapacity = p.MaxCapacity,
                Name = p.Name
            });
        }
    }
}