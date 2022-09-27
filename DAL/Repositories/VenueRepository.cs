using AB.BeerQuest.DataModel;
using AB.BeerQuest.DataModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace AB.BeerQuest.DAL.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly VenueDb _venueDb;

        public VenueRepository(VenueDb venueDb)
        {
            _venueDb = venueDb;
        }

        public async Task<IEnumerable<Venue>> GetAll(SortOrder? sortOrder)
        {
            if (!sortOrder.HasValue)
            {
                return
                    await _venueDb.Venues.ToListAsync();
            }
            else
            {
                return await SortVenues(_venueDb.Venues, sortOrder.Value).ToListAsync();
            }
        }

        public async Task AddRange(IEnumerable<Venue> venues)
        {
            await _venueDb.Venues.AddRangeAsync(venues);
            await _venueDb.SaveChangesAsync();
        }

        public async Task<Venue?> GetById(int id)
        {
            return await _venueDb.Venues.FindAsync(id);
        }

        public async Task<IEnumerable<Venue>> GetByFilters(VenueFilters filters)
        {
            var venues = _venueDb.Venues.AsQueryable();
            if (filters.Categories != null && filters.Categories.Any())
            {
                venues = venues.Where(v => filters.Categories.Contains(v.Category));
            }
            if (filters.StarsAmenities.HasValue)
            {
                venues = venues.Where(v => v.StarsAmenities >= filters.StarsAmenities);
            }
            if (filters.StarsAtmosphere.HasValue)
            {
                venues = venues.Where(v => v.StarsAtmosphere >= filters.StarsAtmosphere);
            }
            if (filters.StarsBeer.HasValue)
            {
                venues = venues.Where(v => v.StarsBeer >= filters.StarsBeer);
            }
            if (filters.StarsValue.HasValue)
            {
                venues = venues.Where(v => v.StarsValue >= filters.StarsValue);
            }
            if (filters.Tags != null && filters.Tags.Any())
            {
                foreach (var tag in filters.Tags)
                {
                    venues =
                        from v in venues
                        join t in _venueDb.VenueTags on v.Id equals t.VenueId
                        where t.Tag == tag
                        select v;
                }
            }
            if (filters.SortOrder.HasValue)
            {
                venues = SortVenues(venues, filters.SortOrder.Value);
            }
            return await venues.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetActiveTags()
        {
            return
                await _venueDb.VenueTags
                    .Select(t => t.Tag)
                    .Distinct()
                    .OrderBy(t => t)
                    .ToListAsync();
        }

        private static IQueryable<Venue> SortVenues(IQueryable<Venue> venues, SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Newest:
                    venues = venues.OrderByDescending(v => v.Date);
                    break;
                case SortOrder.StarsAmenities:
                    venues = venues.OrderByDescending(v => v.StarsAmenities);
                    break;
                case SortOrder.StarsAtmosphere:
                    venues = venues.OrderByDescending(v => v.StarsAtmosphere);
                    break;
                case SortOrder.StarsBeer:
                    venues = venues.OrderByDescending(v => v.StarsBeer);
                    break;
                case SortOrder.StarsValue:
                    venues = venues.OrderByDescending(v => v.StarsValue);
                    break;
            }
            return venues;
        }
    }
}
