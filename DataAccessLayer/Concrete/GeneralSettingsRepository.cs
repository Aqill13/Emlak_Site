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
    public class GeneralSettingsRepository : GenericRepository<GeneralSettings, Context>, IGeneralSettingsRepository
    {
        public GeneralSettingsRepository(Context context) : base(context)
        {
        }
    }
}
