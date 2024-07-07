using BusinessLayer.Abstract;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdvertController : Controller
    {
        IAdvertService _advertService;
        ICityService _cityService;
        IDistrictService _districtService;
        ITypeService _typeService;
        ISituationService _situationService;
        IUserService _userService;
        IImagesService _imagesService;

        public AdvertController(IAdvertService advertService, ICityService cityService,
            IDistrictService districtService, ITypeService typeService,
            ISituationService situationService, IUserService userService, IImagesService imagesService)
        {
            _advertService = advertService;
            _cityService = cityService;
            _districtService = districtService;
            _typeService = typeService;
            _situationService = situationService;
            _userService = userService;
            _imagesService = imagesService;
        }

        public async Task<IActionResult> Index()
        {
            var advertsList = await _advertService.BL_ListAsync(new[] { "Type", "Situation", "City", "District" }, x => x.Status == true || x.Status == false);

            return View(advertsList);
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
        public async Task DropDown()
        {
            var cities = await Cities();
            var situations = await Situations();
            var types = await Types();
            ViewBag.CityList = new SelectList(cities, "Id", "Name");
            ViewBag.SituationList = new SelectList(situations, "Id", "Name");
            ViewBag.TypeList = new SelectList(types, "Id", "Name");
        }
        public async Task DropDown(Advert advert)
        {
            var cities = await Cities();
            var situations = await Situations();
            var types = await Types();
            var districts = await _districtService.BL_ListAsync(x => x.CityId == advert.CityId);
            ViewBag.CityList = new SelectList(cities, "Id", "Name", advert.CityId);
            ViewBag.SituationList = new SelectList(situations, "Id", "Name");
            ViewBag.TypeList = new SelectList(types, "Id", "Name");
            ViewBag.Districts = new SelectList(districts, "Id", "Name");
        }

        //District
        public async Task<IActionResult> GetDistrict(int id)
        {
            var districtList = await _districtService.BL_ListAsync(x => x.Status == true && x.CityId == id);
            ViewBag.DistrictList = new SelectList(districtList, "Id", "Name");
            return PartialView("DistrictPartial");
        }
        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }

        //New advert add process

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["id"] = _userService.GetUserId();

            await DropDown();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Advert data)
        {
            if (!ModelState.IsValid)
            {
                await DropDown(data);
                return View(data);
            }
            foreach (var item in data.Image)
            {
                var filePath = Path.Combine("wwwroot/Pictures/", item.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                data.Images.Add(new Images
                {
                    ImageUrl = item.FileName,
                    Status = true,
                });
            }
            data.Status = true;
            await _advertService.BL_CreateAsync(data);
            TempData["newAdvertSuccess"] = "New Advert Added";
            return RedirectToAction(nameof(Index));
        }

        //Advert delete process

        public async Task<IActionResult> Delete(int id)
        {
            var deleteItem = await _advertService.BL_ReadAsync(id);
            await _advertService.BL_DeleteAsync(deleteItem);
            return RedirectToAction(nameof(Index));
        }

        //Update process

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TempData["id"] = _userService.GetUserId();
            var updateRow = await _advertService.BL_ReadAsync(id);

            await DropDown(updateRow);
            return View(updateRow);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Advert upData)
        {
            if (ModelState.IsValid)
            {
                upData.Images.Clear();
                if (upData.Image != null)
                {
                    foreach (var item in upData.Image)
                    {
                        var filePath = Path.Combine("wwwroot/Pictures/", item.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        upData.Images.Add(new Images
                        {
                            ImageUrl = item.FileName,
                            Status = true,
                        });
                    }
                    await _advertService.BL_UpdateAsync(upData);
                    TempData["updateAdvert"] = "Advert Updated";
                    return RedirectToAction(nameof(Index));
                }
            }
            await DropDown(upData);
            return View(upData);
        }

        //Img Details process

        public async Task<IActionResult> ImgAdverts(int id)
        {
            var images = await _imagesService.GetImagesInAdvertAsync(x => x.AdvertId == id);
            return View(images);
        }

        //Add Image Process

        [HttpGet]
        public async Task<IActionResult> AddImage(int id)
        {
            var advert = await _advertService.BL_ReadAsync(id);
            return View(advert);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(Advert data)
        {
            if (data.Image != null)
            {
                foreach (var item in data.Image)
                {
                    var filePath = Path.Combine("wwwroot/Pictures/", item.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    await _imagesService.BL_CreateAsync(new Images
                    {
                        ImageUrl = item.FileName,
                        Status = true,
                        AdvertId = data.Id
                    });
                }
                TempData["newImgAdded"] = "New Images Added";
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        public async Task<IActionResult> DeleteImg(int id)
        {
            var deleteImg = await _imagesService.BL_ReadAsync(id);
            await _imagesService.BL_DeleteAsync(deleteImg);
            return RedirectToAction(nameof(ImgAdverts));
        }

    }
}
