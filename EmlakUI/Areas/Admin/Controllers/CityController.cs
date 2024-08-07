using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CityController : Controller
    {
        ICityService _cityService;
        IDistrictService _districtService;

        public CityController(ICityService cityService, IDistrictService districtService)
        {
            _cityService = cityService;
            _districtService = districtService;
        }

        public async Task<IActionResult> Index()
        {
            var cityList = await _cityService.BL_ListAsync();
            return View(cityList);
        }

        //Create new City

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(City data)
        {
            if (ModelState.IsValid)
            {
                data.Status = true;
                await _cityService.BL_CreateAsync(data);
                TempData["newCitySuccess"] = "New City Added";
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(e => e.Errors);
            ViewData["Errors"] = errors;
            return View(data);
        }

        //City Details Process

        public async Task<IActionResult> Details(int id)
        {
            var districtInCityList = await _districtService.BL_ListAsync(x => x.CityId == id && x.Status == true);
            var cityRow = await _cityService.BL_ReadAsync(id);
            var title = cityRow.Name;
            ViewBag.tit = title;
            return View(districtInCityList);
        }

        //City Delete Process

        public async Task<IActionResult> Delete(int id)
        {
            var deleteCityItem = await _cityService.BL_ReadAsync(id);
            await _cityService.BL_DeleteAsync(deleteCityItem);
            return RedirectToAction(nameof(Index));
        }

        //City Update Process

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateElement = await _cityService.BL_ReadAsync(id);
            return View(updateElement);
        }
        [HttpPost]
        public async Task<IActionResult> Update(City upData)
        {
            if (ModelState.IsValid)
            {
                var getDistrictsInCity = await _districtService.BL_ListAsync(x => x.CityId == upData.Id);
                foreach (var district in getDistrictsInCity)
                {
                    if(upData.Status)
                    {
                        district.Status = true;
                        await _districtService.BL_UpdateAsync(district);
                    }
                    else
                    {
                        district.Status = false;
                        await _districtService.BL_UpdateAsync(district);
                    }
                }
                await _cityService.BL_UpdateAsync(upData);
                TempData["updateCity"] = "City Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(upData);
        }
    }
}
