using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.iRepository;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : iVillaRepository
    {
        private readonly ApplicationDbContext _db; //add db context using dependancy context
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Villa Entity)
        {
            await _db.Villas.AddAsync(Entity);
            await Save();

        }

        public Task<Villa> Get(Expression<Func<Villa>> filter = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<Villa>> GetAll(Expression<Func<Villa>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Villa Entity)
        {
            _db.Villas.Remove(Entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
