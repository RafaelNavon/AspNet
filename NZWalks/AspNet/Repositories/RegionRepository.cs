using AspNet.Data;
using AspNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
