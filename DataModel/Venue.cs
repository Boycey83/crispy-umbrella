using AB.BeerQuest.DataModel.Enums;

namespace AB.BeerQuest.DataModel
{
    public record Venue
    {
        public Venue()
        {
            Tags = new List<VenueTag>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public Category Category { get; set; }
        public string? Url { get; set; }
        public DateTime Date { get; set; }
        public string? Excerpt { get; set; }
        public string? Thumbnail { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Twitter { get; set; }
        public float StarsBeer { get; set; }
        public float StarsAtmosphere { get; set; }
        public float StarsAmenities { get; set; }
        public float StarsValue { get; set; }
        public ICollection<VenueTag> Tags { get; set; }
    }
}
