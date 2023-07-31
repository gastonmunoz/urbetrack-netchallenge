using System;

namespace NetChallenge.Domain
{
    public class Booking
    {
        public Office Office { get; set; }
        public DateTime Datetime { get; set; }
        public int Hours { get; set; }
        public string User { get; set; }
    }
}