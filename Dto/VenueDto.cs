using AB.BeerQuest.DataModel.Enums;
using System.ComponentModel.DataAnnotations;

namespace AB.BeerQuest.Dto
{
    public record VenueDto
    {
        public VenueDto()
        {
            Tags = new List<string>();
        }

        [Key]
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
        public IEnumerable<string> Tags { get; set; }
    }
}
