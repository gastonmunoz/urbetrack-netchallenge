using System;

namespace NetChallenge.Domain
{
    public class Booking
    {
        public Location Location { get; set; }
        public Office Office { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string UserName { get; set; }
    }
}