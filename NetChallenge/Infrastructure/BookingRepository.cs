using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Exceptions;

namespace NetChallenge.Infrastructure
{
    /// <summary>
    /// Repositorio de reuniones (Booking)
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        private IList<Booking> Bookings { get; set; } = new List<Booking>();

        /// <summary>
        /// Devuelve todas las reuniones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Booking> AsEnumerable()
        {
            return Bookings;
        }

        /// <summary>
        /// Agrega una reunión
        /// </summary>
        /// <param name="item">Reunión a agregar</param>
        /// <exception cref="InvalidOfficeNameException"></exception>
        /// <exception cref="InvalidDurationException"></exception>
        /// <exception cref="BookingWithoutUserNameException"></exception>
        /// <exception cref="ExistingBookingAtSameDateTimeException"></exception>
        /// <exception cref="InvalidLocationNameException"></exception>
        public void Add(Booking item)
        {
            DateTime start = item.DateTime;
            DateTime end = item.DateTime.Add(item.Duration);

            Booking existing = item.Office?.Bookings.FirstOrDefault(p => start < p.DateTime.Add(p.Duration) && p.DateTime < end && p.Office.Name == item.Office.Name);

            if (item.Office == null)
            {
                throw new InvalidOfficeNameException();
            } else if (item.Duration <= TimeSpan.Zero)
            {
                throw new InvalidDurationException();
            } else if (string.IsNullOrWhiteSpace(item.UserName))
            {
                throw new BookingWithoutUserNameException();
            } else if (existing != null)
            {
                throw new ExistingBookingAtSameDateTimeException(existing.UserName);
            } else if (item.Location == null)
            {
                throw new InvalidLocationNameException("La reunión tiene un nombre de local inexistente.");
            }

            item.Office.Bookings.Add(item);
            Bookings.Add(item);
        }
    }
}