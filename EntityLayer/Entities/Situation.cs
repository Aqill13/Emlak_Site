using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Situation
    {
        //kiraye,satiliq ve.s
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public virtual List<Advert?> Adverts { get; set; }
    }
}
