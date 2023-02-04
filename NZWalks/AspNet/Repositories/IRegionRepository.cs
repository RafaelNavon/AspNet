using AspNet.Models.Domain;

namespace AspNet.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
    }
}
