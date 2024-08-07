using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Advert
    {
        public Advert()
        {
            Images = new List<Images>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int Area { get; set; }
        public int NumberOfRooms { get; set; }
        public int Floor { get; set; }
        public int fullFloor { get; set; }
        public decimal Price { get; set; }
        public string? PriceType { get; set; }
        public string Address { get; set; }
        public DateTime AdvertDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public bool Elevator { get; set; }
        public bool Pool { get; set; }
        public bool Credit { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int SituationId { get; set; }
        public string UserAdminId { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile?> Image { get; set; } = new List<IFormFile>();
        public bool Status { get; set; }
        public string PhoneNumber { get; set; }
        public virtual City? City { get; set; }
        public virtual District? District { get; set; }
        public virtual Situation? Situation { get; set; }
        public virtual UserAdmin? UserAdmin { get; set; }
        public virtual Type? Type { get; set; }
        public virtual List<Images?> Images { get; set; }
    }
}
