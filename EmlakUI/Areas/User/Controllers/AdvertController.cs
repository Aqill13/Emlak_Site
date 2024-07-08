using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakUI.Areas.User.Controllers
{
    [Area("User")]
    //[Authorize(Roles = "User")]
    public class AdvertController : Controller
    {
        IAdvertService _advertService;
        IUserService _userService;
        ICityService _cityService;
        IDistrictService _districtService;
        ISituationService _situationService;
        ITypeService _typeService;
        IImagesService _imagesService;

        public AdvertController(IAdvertService advertService, IUserService userService, 
            ICityService cityService, IDistrictService districtService, ISituationService situationService, 
            ITypeService typeService, IImagesService imagesService)
        {
            _advertService = advertService;
            _userService = userService;
            _cityService = cityService;
            _districtService = districtService;
            _situationService = situationService;
            _typeService = typeService;
            _imagesService = imagesService;
        }

        public async Task<IActionResult> Index()
        {
            string id = HttpContext.Session.GetString("Id");
            var advertList = await _advertService.BL_ListAsync(new[] { "Type", "Situation", "City", "District" }, x => x.Status == true && x.UserAdminId == id);
            return View(advertList);
        }

        //City List
        public async Task<List<City>> Cities()
        {
            List<City> cities = await _cityService.BL_ListAsync(x => x.Status == true);
            return cities;
        }
        //Situation List
        public async Task<List<Situation>> Situations()
        {
            List<Situation> situations = await _situationService.BL_ListAsync(x => x.Status == true);
            return situations;
        }
        //Type List
        public async Task<List<EntityLayer.Entities.Type>> Types()
        {
            List<EntityLayer.Entities.Type> types = await _typeService.BL_ListAsync(x => x.Status == true);
            return types;
        }

        //District in city
        public async Task<IActionResult> GetDistricts(int id)
        {
            var districtList = await _districtService.BL_ListAsync(x => x.Status == true && x.CityId == id);
            ViewBag.DistrictList = new SelectList(districtList, "Id", "Name");
            return PartialView("DistrictPartial");
        }
        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }

        //Dropdown
        public async Task Dropdown()
        {
            var cities = await Cities();
            var situations = await Situations();
            var types = await Types();
            ViewBag.CityList = new SelectList(cities, "Id", "Name");
            ViewBag.SituationList = new SelectList(situations, "Id", "Name");
            ViewBag.TypeList = new SelectList(types, "Id", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["id"] = HttpContext.Session.GetString("Id");
            await Dropdown();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Advert data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            foreach (var img in data.Image)
            {
                var filePath = Path.Combine("wwwroot/UserPictures/", img.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                data.Images.Add(new Images
                {
                    ImageUrl = img.FileName,
                    Status = true
                });
            }
            data.Status = true;
            await _advertService.BL_CreateAsync(data);
            TempData["newAdvertSuccess"] = "New Advert Added";
            return RedirectToAction(nameof(Index));
        }
    }
}
