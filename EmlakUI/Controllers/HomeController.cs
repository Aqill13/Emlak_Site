using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmlakUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly IImagesService imagesService;
        private readonly ICityService cityService;
        private readonly IDistrictService districtService;
        private readonly ITypeService typeService;
        private readonly ISituationService situationService;
        private readonly IUsersService usersService;

        public HomeController(IAdvertService advertService, IImagesService imagesService,
            ICityService cityService, IDistrictService districtService, ITypeService typeService, 
            IUsersService usersService, ISituationService situationService)
        {
            this.advertService = advertService;
            this.imagesService = imagesService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.typeService = typeService;
            this.usersService = usersService;
            this.situationService = situationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdvertByType(int id)
        {
            var advertListByType = await advertService.BL_ListAsync(x => x.TypeId == id);

            foreach (var item in advertListByType)
            {
                var imgs = await imagesService.BL_ListAsync(x => x.AdvertId == item.Id);
                item.Images = imgs.ToList();

                var city = await cityService.BL_ReadAsync(item.CityId);
                item.City = city;

                var district = await districtService.BL_ReadAsync(item.DistrictId);
                item.District = district;
            }

            var typeName = await typeService.BL_ReadAsync(id);
            ViewBag.typeName = typeName.Name;

            return View(advertListByType);
        }
        public async Task<IActionResult> AdvertBySituation(int? id)
        {
            var advertList = new List<Advert>();

            if (id != null)
            {
                advertList = await advertService.BL_ListAsync(x => x.SituationId == id);
                ViewBag.situationId = id;
            }
            else
            {
                advertList = await advertService.BL_ListAsync();
            }
            foreach (var item in advertList)
            {
                var imgs = await imagesService.BL_ListAsync(x => x.AdvertId == item.Id);
                item.Images = imgs.ToList();

                var city = await cityService.BL_ReadAsync(item.CityId);
                item.City = city;

                var district = await districtService.BL_ReadAsync(item.DistrictId);
                item.District = district;
            }

            return View(advertList);
        }

        
    }
}
