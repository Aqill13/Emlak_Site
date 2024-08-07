using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmlakUI.ViewComponents
{
    public class SomeAdvertList : ViewComponent
    {
        private readonly IAdvertService _advertService;
        private readonly IImagesService _imagesService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public SomeAdvertList(IAdvertService advertService, IImagesService imagesService, 
            ICityService cityService, IDistrictService districtService)
        {
            _advertService = advertService;
            _imagesService = imagesService;
            _cityService = cityService;
            _districtService = districtService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var advertList = await _advertService.BL_ListAsync();
            var someAdvertList = advertList.OrderByDescending(x => x.AdvertDate).Take(12).ToList();

            foreach (var item in someAdvertList)
            {
                var images = await _imagesService.BL_ListAsync(x => x.AdvertId == item.Id);
                item.Images = images.ToList();

                var cityName = await _cityService.BL_ReadAsync(item.CityId);
                var districtName = await _districtService.BL_ReadAsync(item.DistrictId);
                item.City = cityName;
                item.District = districtName;
            }

            return View(someAdvertList);
        }
    }
}
