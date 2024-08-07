using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.ViewComponents
{
    public class ScrollRentPropertyInHome : ViewComponent
    {
        private readonly IAdvertService _advertService;
        private readonly IImagesService _imageService;

        public ScrollRentPropertyInHome(IAdvertService advertService, IImagesService imageService)
        {
            _advertService = advertService;
            _imageService = imageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rentList = await _advertService.BL_ListAsync(x => x.SituationId == 1);

            foreach (var rent in rentList)
            {
                var images = await _imageService.BL_ListAsync(x => x.AdvertId == rent.Id);

                rent.Images = images.ToList();
            }

            return View(rentList);
        }
    }
}
