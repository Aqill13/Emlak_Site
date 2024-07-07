using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;

        public DistrictController(IDistrictService districtService, ICityService cityService)
        {
            _districtService = districtService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            var districtList = await _districtService.BL_ListAsync("City");
            return View(districtList);
        }

        public async Task<List<City>> Cities()  
        {
            var cityList = await _cityService.BL_ListAsync(x => x.Status == true);
            return cityList;
        }

        public async Task DropDown()
        {
            var cities = await Cities();
            ViewBag.cityList = new SelectList(cities, "Id", "Name");
        }
        //Add new District

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await DropDown();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(District data)
        {
            if (ModelState.IsValid)
            {
                data.Status = true;
                await _districtService.BL_CreateAsync(data);
                TempData["addDistrict"] = "New District Added";
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewData["Errors"] = errors;
            await DropDown();
            return View(data);
        }

        //Delete District Process

        public async Task<IActionResult> Delete(int id)
        {
            var deleteRow = await _districtService.BL_ReadAsync(id);
            await _districtService.BL_DeleteAsync(deleteRow);
            return RedirectToAction(nameof(Index));
        }

        //Update District Process

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var upRow = await _districtService.BL_ReadAsync(id);
            await DropDown();
            return View(upRow);
        }

        [HttpPost]
        public async Task<IActionResult> Update(District upData)
        {
            if (ModelState.IsValid)
            {
                await _districtService.BL_UpdateAsync(upData);
                TempData["updateDistrict"] = "District Updated";
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewData["Errors"] = errors;
            await DropDown();
            return View(upData);
        }
    }
}
