using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public int AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}
