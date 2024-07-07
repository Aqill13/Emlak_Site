using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public virtual List<District> Districts { get; set; } = new List<District>();
        public virtual List<Advert> Adverts { get; set; } = new List<Advert>();
    }
}
