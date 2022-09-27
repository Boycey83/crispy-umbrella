using AB.BeerQuest.DataModel.Enums;
using AB.BeerQuest.DataModel;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace AB.BeerQuest.CsvHelper
{
    public class VenueMap : ClassMap<Venue>
    {
        public VenueMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.Name).Name("name");
            Map(m => m.Category).Name("category").TypeConverter<CategoryEnumConverter>();
            Map(m => m.Url).Name("url");
            Map(m => m.Date).Name("date");
            Map(m => m.Excerpt).Name("excerpt");
            Map(m => m.Thumbnail).Name("thumbnail");
            Map(m => m.Lat).Name("lat");
            Map(m => m.Lng).Name("lng");
            Map(m => m.Address).Name("address");
            Map(m => m.Phone).Name("phone");
            Map(m => m.Twitter).Name("twitter");
            Map(m => m.StarsBeer).Name("stars_beer");
            Map(m => m.StarsAtmosphere).Name("stars_atmosphere");
            Map(m => m.StarsAmenities).Name("stars_amenities");
            Map(m => m.StarsValue).Name("stars_value");
            Map(m => m.Tags).Name("tags").TypeConverter<VenueTagConverter>();
        }
    }

    class CategoryEnumConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text)
            {
                case "Closed venues":
                    return Category.ClosedVenues;
                case "Bar reviews":
                    return Category.BarReviews;
                case "Pub reviews":
                    return Category.PubReviews;
                case "Other reviews":
                    return Category.OtherReviews;
                case "Uncategorized":
                    return Category.Uncategorized;
                default:
                    return Category.Unknown;
            }
        }
    }
    class VenueTagConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Array.Empty<VenueTag>();
            }
            return text
                .Split(",")
                .Select(t => new VenueTag { Tag = t })
                .ToList();
        }
    }
}
