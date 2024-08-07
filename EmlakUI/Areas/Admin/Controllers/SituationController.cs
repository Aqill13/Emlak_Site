using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class SituationController : Controller
    {
        private readonly ISituationService _situationService;

        public SituationController(ISituationService situationService)
        {
            _situationService = situationService;
        }

        public async Task<IActionResult> Index()
        {
            var SituationList = await _situationService.BL_ListAsync();
            return View(SituationList);
        }

        //Delete 
        public async Task<IActionResult> Delete(int id)
        {
            var deleteRow = await _situationService.BL_ReadAsync(id);
            await _situationService.BL_DeleteAsync(deleteRow);
            return RedirectToAction(nameof(Index), "Situation");
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Situation data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                ViewData["Errors"] = errors;
                return View(data);
            }
            data.Status = true;
            await _situationService.BL_CreateAsync(data);
            TempData["newSituationSuccess"] = "New Situation Added";
            return RedirectToAction(nameof(Index), "Situation");
        }

        //Update 

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var upRow = await _situationService.BL_ReadAsync(id);
            return View(upRow);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Situation upData)
        {
            if (ModelState.IsValid)
            {
                await _situationService.BL_UpdateAsync(upData);
                TempData["updateSituation"] = "Situation Updated";
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            ViewData["Errors"] = errors;
            return View(upData);
        }
    }
}
