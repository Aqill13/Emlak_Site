using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Favorites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AdvertId { get; set; }
        public virtual List<Advert> Advert { get; set; } = new List<Advert>();
    }
}
