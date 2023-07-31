using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Infrastructure;

namespace NetChallenge
{
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

        public void AddLocation(AddLocationRequest request)
        {
            Location location = new Location
            {
                Name = request.Name,
                Neighborhood = request.Neighborhood
            };
            _locationRepository.Add(location);
        }

        public void AddOffice(AddOfficeRequest request)
        {
            Location location = _locationRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.LocationName);

            Office office = new Office
            {
                Name = request.Name,
                MaxCapacity = request.MaxCapacity,
                AvailableResources = request.AvailableResources?.ToArray(),
                Location = location
            };
            _officeRepository.Add(office);
        }

        public void BookOffice(BookOfficeRequest request)
        {
            Location location = _locationRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.LocationName);
            Office office = _officeRepository.AsEnumerable().FirstOrDefault(p => p.Name == request.OfficeName);

            Booking booking = new Booking
            {
                DateTime = request.DateTime,
                Duration = request.Duration,
                Office = office,
                Location = location,
                UserName = request.UserName
            };

            _bookingRepository.Add(booking);
        }

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

        public IEnumerable<LocationDto> GetLocations()
        {
            return _locationRepository.AsEnumerable().Select(p => new LocationDto()
            {
                Name = p.Name,
                Neighborhood = p.Neighborhood,
            });
        }

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