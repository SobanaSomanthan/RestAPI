using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.iRepository
{
    public interface iVillaRepository
    {
        Task<List<Villa>> GetAll(Expression<Func<Villa>> filter=null);
        Task<Villa> Get(Expression<Func<Villa>> filter = null, bool tracked=true); //for AsNoTracking we used bool tracked
        Task Create(Villa Entity);
        Task Remove(Villa Entity);
        Task Save();
    }
}
