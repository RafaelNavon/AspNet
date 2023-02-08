using AspNet.Data;
using AspNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var exisitingWalk = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (exisitingWalk == null)
            {
                return null;
            }
            nZWalksDbContext.WalkDifficulty.Remove(exisitingWalk);
            await nZWalksDbContext.SaveChangesAsync();
            return exisitingWalk;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
                
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            //Find the current exsiting walkDifficulty by ID
            var exisitingWalk = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if(exisitingWalk == null)
            {
                return null;
            }

            exisitingWalk.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return exisitingWalk;
        }
    }
}
