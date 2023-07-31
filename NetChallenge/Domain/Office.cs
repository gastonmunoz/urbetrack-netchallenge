namespace NetChallenge.Domain
{
    public class Office
    {
        public Location Location { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public string[] AvailableResources { get; set; }

    }
}