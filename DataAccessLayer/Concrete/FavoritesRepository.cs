using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class FavoritesRepository : GenericRepository<Favorites, Context>, IFavoritesRepository
    {
        public FavoritesRepository(Context context) : base(context)
        {
        }
    }
}
