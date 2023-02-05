using AspNet.Models.Domain;

namespace AspNet.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region>GetAsync(Guid id);

        Task<Region>AddAsync(Region region);

        Task<Region>DeleteAsync(Guid id);

        Task<Region>UpdatedAsync(Guid id, Region region);
    }
}
