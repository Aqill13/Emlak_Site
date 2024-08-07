using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Controllers
{
    public class MoreController : Controller
    {
        private readonly IAdvertService advertService;
        private readonly IImagesService imagesService;
        private readonly ICityService cityService;
        private readonly IDistrictService districtService;
        private readonly ITypeService typeService;
        private readonly IUsersService usersService;

        public MoreController(IAdvertService advertService, IImagesService imagesService,
            ICityService cityService, IDistrictService districtService, ITypeService typeService, IUsersService usersService)
        {
            this.advertService = advertService;
            this.imagesService = imagesService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.typeService = typeService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var advert = await advertService.BL_ReadAsync(id);

            var imgListInAdvert = await imagesService.BL_ListAsync(x => x.AdvertId == id);

            var city = await cityService.BL_ReadAsync(advert.CityId);
            var district = await districtService.BL_ReadAsync(advert.DistrictId);
            var type = await typeService.BL_ReadAsync(advert.TypeId);

            var user = await usersService.BL_ReadStringId(advert.UserAdminId);

            advert.UserAdmin = user;
            advert.Type = type;
            advert.City = city;
            advert.District = district;
            advert.Images = imgListInAdvert.ToList();
            
            return View(advert);
        }
    }
}
