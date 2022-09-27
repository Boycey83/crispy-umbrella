using AB.BeerQuest.DataModel;
using AB.BeerQuest.DataModel.Enums;

namespace AB.BeerQuest.DAL.Repositories
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAll(SortOrder? sortOrder);
        Task AddRange(IEnumerable<Venue> venues);
        Task<Venue?> GetById(int id);
        Task<IEnumerable<Venue>> GetByFilters(VenueFilters filters);
        Task<IEnumerable<string>> GetActiveTags();
    }
}
