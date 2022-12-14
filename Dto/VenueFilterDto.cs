using AB.BeerQuest.DataModel.Enums;

namespace AB.BeerQuest.Dto
{
    public class VenueFiltersDto
    {
        public IEnumerable<Category>? Categories { get; set; }
        public float? StarsBeer { get; set; }
        public float? StarsAtmosphere { get; set; }
        public float? StarsAmenities { get; set; }
        public float? StarsValue { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}
