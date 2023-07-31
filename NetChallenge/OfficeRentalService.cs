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
            throw new NotImplementedException();
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}