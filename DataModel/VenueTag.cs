namespace AB.BeerQuest.DataModel
{
    public class VenueTag
    {
        public VenueTag()
        {
            Tag = string.Empty;
        }

        public int Id { get;set; }
        public Venue? Venue { get; set; }
        public int VenueId { get; set; }
        public string Tag { get; set; }
    }
}
