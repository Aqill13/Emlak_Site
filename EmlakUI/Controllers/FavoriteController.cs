using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmlakUI.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IFavoritesService _favoritesService;

        public FavoriteController(IAdvertService advertService, IFavoritesService favoritesService)
        {
            _advertService = advertService;
            _favoritesService = favoritesService;
        }

        public async Task<IActionResult> AddFavorite(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var t = await _favoritesService.BL_ListAsync(x => x.UserId == userId && x.AdvertId == id);
                if (t.Count == 0)
                {
                    var favorites = new Favorites()
                    {
                        AdvertId = id,
                        UserId = userId
                    };
                    await _favoritesService.BL_CreateAsync(favorites);

                    return Json(new { success = true, message = "Seçilmişlərə əlavə olundu." });
                }
                else
                {
                    await _favoritesService.BL_DeleteAsync(t.First());
                    return Json(new { success = false, message = "Seçilmişlərdən silindi" });
                }
            }

            return Json(new { success = false, message = "Seçilmişlərə əlavə etmək üçün daxil olmalısınız!" });
        }
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var deleteFavoriRow = await _favoritesService.BL_ReadAsync(id);
            await _favoritesService.BL_DeleteAsync(deleteFavoriRow);
            return Json(new { success = true, message = "Seçilmişlərdən silindi!" });
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFavoriList = await _favoritesService.BL_ListAsync(x => x.UserId == userId);

            foreach (var item in userFavoriList)
            {
                var adverts = await _advertService.BL_ListAsync(x => x.Id == item.AdvertId);

                item.Advert = adverts.ToList();
            }

            return View(userFavoriList);
        }
    }
}
